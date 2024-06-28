using OLTMockServer.DataStructures;
using OLTMockServer.MockServers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace OLTMockServer
{
    internal class AppManager : IDisposable, IOrderRepository
    {
        private readonly List<TestManager> tests;
        private readonly List<MockServer> servers;
        public static AppManager Current { get; private set; }

        public AppManager()
        {
            this.tests = new List<TestManager>();
            this.servers = new List<MockServer>();

            LoadAvailableServers();

            StartServersApiService();

            AppManager.Current = this;
        }

        private void StartServersApiService()
        {
            foreach (var server in servers)
            {
                if (!server.StartServer())
                {
                    //log... server not started. 'name' 
                }
                else
                {
                    //log...server started./type/name/url...
                }
            }
        }

        private void LoadAvailableServers()
        {
            servers.Add(new SnapMockServer((IOrderRepository)this));
        }

        internal void AddTest(TestManager newTest)
        {
            tests.Add(newTest);

            UpdateTestList();
        }

        public TestManager CreateTestManager(Definitions.KnownOnlineShops onlineShop)
        {
            switch (onlineShop)
            {
                case Definitions.KnownOnlineShops.Snap:
                    var server = (SnapMockServer)GetOnlineShopServer(onlineShop);
                    return new SnapTestManager(server);

                case Definitions.KnownOnlineShops.Digi:
                    return null;
                case Definitions.KnownOnlineShops.None:
                default:
                    throw new ApplicationException("Unknown online shop type");
            }
        }

        public TestManager AddTest(Definitions.KnownOnlineShops onlineShop, bool onlyCreate = false)
        {
            var testManager = CreateTestManager(onlineShop);

            if (!onlyCreate)
            {
                tests.Add(testManager);

                UpdateTestList();
            }

            return testManager;
        }

        private MockServer GetOnlineShopServer(Definitions.KnownOnlineShops onlineShop)
        {
            var server = servers.FirstOrDefault(s => s.OnlineShopType == onlineShop);

            if (server == null)
            {
                throw new ApplicationException("Unknown online shop type");
            }

            return server;
        }

        private int lastTestNumber;
        public int GetNextTestNumber()
        {
            return ++lastTestNumber;
        }

        public List<Definitions.KnownOnlineShops> AvailableServerTypes
        {
            get
            {
                return servers.Select(s => s.OnlineShopType).ToList();
            }
        }

        public void Dispose()
        {
            foreach (var server in servers)
            {
                server.StopServer();
            }
        }

        public Order FindOrder(string code)
        {
            foreach (var test in this.tests)
            {
                var lastOrderWithRequestedCode = test.TestProject.Orders.FindLast(o => o.Code == code);

                if (lastOrderWithRequestedCode != null)
                {
                    return lastOrderWithRequestedCode;
                }
            }

            throw new ApplicationException($"An order with this code was not found, make sure that the test project related to this order is open on the server.");
        }

        public class DelayedAction
        {
            public DelayedAction(Action action, TimeSpan delay)
            {
                this.Action = action;
                this.DelayTime = delay;
                this.startTime = DateTime.Now;
                ResetTask();
            }

            private void ResetTask()
            {
                this.task = Task.Factory.StartNew(() =>
                {
                    while ((DateTime.Now - this.startTime).TotalSeconds < this.DelayTime.TotalSeconds)
                    {
                        Thread.Sleep(100);
                    }

                    taskStarted = true;
                    this.Action();
                });
            }
            bool taskStarted = false;
            private Task task;
            private DateTime startTime;
            public TimeSpan DelayTime { get; set; }
            public Action Action { get; set; }

            public void Delay(TimeSpan amount)
            {
                this.startTime = DateTime.Now;
                this.DelayTime = this.DelayTime.Add(amount);
                if (task == null || task.IsCompleted || taskStarted)
                {
                    ResetTask();
                }
            }
        }

        private Dictionary<string, DelayedAction> delayedActions = new Dictionary<string, DelayedAction>();

        public void SaveOrder(Order order)
        {
            TestManager relatedTest = null;

            foreach (var test in this.tests)
            {
                foreach (var o in test.TestProject.Orders)
                {
                    if (o.UId == order.UId)
                    {
                        relatedTest = test;

                        if (delayedActions.ContainsKey("Save"))
                        {
                            delayedActions["Save"].Delay(TimeSpan.FromSeconds(2));
                        }
                        else
                        {
                            delayedActions["Save"] =
                                new DelayedAction(
                                    () => relatedTest.SaveTestProject(),
                                    TimeSpan.FromSeconds(2)
                                    );
                        }

                        return;
                    }
                }
            }

            throw new ApplicationException($"An order with this code was not found, make sure that the test project related to this order is open on the server.");
        }

        public void RemoveTest(TestManager test)
        {
            tests.Remove(test);
            UpdateTestList();
        }

        public void UpdateTestList()
        {
            var appDataFilePath = Path.Combine(Path.GetTempPath(), "OltMockServer");

            if (!Directory.Exists(appDataFilePath))
            {
                Directory.CreateDirectory(appDataFilePath);
            }

            appDataFilePath = Path.Combine(appDataFilePath, "ActiveTests.dat");

            StringBuilder sb = new StringBuilder();

            foreach (var test in tests)
            {
                if (test.TestProject.IsTemp)
                {
                    sb.AppendLine(test.TestProject.TempFilePath);
                }
                else
                {
                    sb.AppendLine(test.TestProject.SaveFilePath);
                }
            }

            File.WriteAllText(appDataFilePath, sb.ToString());
        }

        public List<TestManager> LoadLastOpenedTestProjects()
        {
            var appDataFilePath = Path.Combine(Path.GetTempPath(), "OltMockServer");

            var result = new List<TestManager>();

            if (!Directory.Exists(appDataFilePath))
            {
                return result;
            }

            appDataFilePath = Path.Combine(appDataFilePath, "ActiveTests.dat");

            if (File.Exists(appDataFilePath))
            {
                var tesProjectPaths = File.ReadAllLines(appDataFilePath);

                foreach (var filePath in tesProjectPaths)
                {
                    if (File.Exists(filePath))
                    {
                        try
                        {
                            var testProject = this.ImportTestFromFile(filePath);
                            result.Add(testProject);
                        }
                        catch (Exception ex)
                        {
                            //log...
                        }

                    }
                }
            }

            return result;
        }

        public TestManager ImportTestFromFile(string filePath)
        {
            var projFilePath = filePath;

            var defaultProj = XMLDataSerializer.Deserialize<TestProject>(projFilePath);

            var fullProj = CreateTestManager(defaultProj.OnlineShop);

            fullProj.ImportFromFile(projFilePath);

            if (fullProj != null)
            {
                AddTest(fullProj);
            }

            return fullProj;
        }
    }
}

//public class Food { }
//public class Meat : Food { }
//public class Grass : Food { }

//public interface IAnimal<out T>
//    where T : Food
//{ }

//public abstract class Animal<TFood> : IAnimal<TFood>
//    where TFood : Food
//{ }

//public class Lion : Animal<Meat> { }

//public class Goat : Animal<Grass> { }

//public class World
//{
//    public List<IAnimal<Food>> Animals { get; set; }

//    public World()
//    {
//        Animals.Add(new Lion());
//        Animals.Add(new Goat());
//    }

//    private void Check()
//    {

//    }
//}