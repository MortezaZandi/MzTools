using System.Collections.Generic;

namespace KyanSetup
{
    public class Feature
    {
        public Feature()
        {
            this.DependsOn = new List<Feature>();
        }

        public int FeatureID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        //this features must be installed if you want install this feature
        public List<Feature> DependsOn { get; set; }

        public bool Selected { get; set; }
        public int ApproximateSizeMB { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
