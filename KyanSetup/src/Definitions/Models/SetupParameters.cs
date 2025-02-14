using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;

namespace KyanSetup
{
    public class SetupParameters
    {
        public SetupParameters()
        {
            InstallationType = InstallationTypes.InstallHeadquarter;
            Configs = new List<RMCConfig>();
            SelectedFeatures = new List<Feature>();
            DatabaseName = "CMSDB";
            DatabasePassword= "sa";
            RMCOperatorName = "Admin";

        }

        public string CMSPath { get; set; }
        public InstallationTypes InstallationType { get; set; }
        public string DatabaseName { get; set; }
        public string DatabasePassword { get; set; }
        public string DatabaseUserName { get; set; }
        public string DatabaseServerName { get; set; }
        public string DefaultLanguage { get; set; }
        public string MakeCDFilePath { get; set; }
        public string SetupFilePath { get; set; }
        public string SetupFileUrl { get; set; }
        public List<Feature> SelectedFeatures { get; set; }
        public string CompanyName { get; set; }
        public string RMCOperatorName { get; set; }
        public string RMCOperatorPassword { get; set; }
        public List<RMCConfig> Configs { get; set; }

    }
}
