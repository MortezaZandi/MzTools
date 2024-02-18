namespace MMF_IPC
{
    public class OLTDiag
    {
        private readonly MLogger logg_action_a;
        private readonly MLogger logg_action_b;
        private readonly MLogger logg_action_c;

        public OLTDiag()
        {
            this.logg_action_a = new MLogger("actiona");
            this.logg_action_b = new MLogger("actionb");
            this.logg_action_c = new MLogger("actionc");

            this.logg_action_a.Create();
            this.logg_action_b.Create();
            this.logg_action_c.Create();
        }

        public void OnActionAStart()
        {
            this.logg_action_a.WriteNewLog("start");
        }

        public void OnActionBStart()
        {
            this.logg_action_b.WriteNewLog("start");
        }

        public void OnActionCStart()
        {
            this.logg_action_c.WriteNewLog("start");
        }

        public void OnActionAStop()
        {
            this.logg_action_a.WriteNewLog("stop");
        }

        public void OnActionBStop()
        {
            this.logg_action_b.WriteNewLog("stop");
        }

        public void OnActionCStop()
        {
            this.logg_action_c.WriteNewLog("stop");
        }

    }
}
