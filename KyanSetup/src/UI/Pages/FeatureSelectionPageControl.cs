using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KyanSetup
{
    public partial class FeatureSelectionPageControl : BasePageControl
    {
        public FeatureSelectionPageControl()
        {
            InitializeComponent();
            InitFeaturesList();
            forceSelectDependantFeatures = true;
        }

        private List<Feature> features;
        private bool forceSelectDependantFeatures;

        private void InitFeaturesList()
        {
            var netframework = new Feature { FeatureID = 0, Name = ".Net Framework 4.8", Description = "RMC foundation tools", ApproximateSizeMB = 200 };
            var rmc = new Feature { FeatureID = 0, Name = "RMC", Description = "Main kyan cms software", ApproximateSizeMB = 1600 };
            var sap = new Feature { FeatureID = 0, Name = "SAPIntegration", Description = "Import data from sap to rmc and export data from rmc to sap", ApproximateSizeMB = 10 };
            var olt = new Feature { FeatureID = 0, Name = "OLT", Description = "Kyan solution for integrate with Snapp Express and Digi Jet", ApproximateSizeMB = 50 };
            var monitoring = new Feature { FeatureID = 0, Name = "Monitoring", Description = "Monitor retail store patch status", ApproximateSizeMB = 10 };
            var crystalReport = new Feature { FeatureID = 0, Name = "CrystalReport", Description = "Kyan default report printing software", ApproximateSizeMB = 250 };

            rmc.DependsOn.Add(netframework);
            sap.DependsOn.Add(rmc);
            olt.DependsOn.Add(rmc);
            monitoring.DependsOn.Add(rmc);

            features = new List<Feature>() {

                netframework, rmc, sap, olt, monitoring, crystalReport
            };

            checkedListBox1.Items.Clear();
            checkedListBox1.Items.AddRange(features.ToArray());
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            var item = checkedListBox1.Items[e.Index];
            var feature = item as Feature;

            if (e.NewValue == CheckState.Checked)
            {
                WhenFeatureSelected(feature);
            }
            else if (e.NewValue == CheckState.Unchecked)
            {
                WhenFeatureUnSelected(feature);
            }

            UpdateRequiredSpaceOnDisk();
        }

        private void WhenFeatureSelected(Feature feature)
        {
            if (forceSelectDependantFeatures)
            {
                foreach (var f in feature.DependsOn)
                {
                    SelectFeature(f);
                }
            }

            feature.Selected = true;
        }

        private void WhenFeatureUnSelected(Feature feature)
        {
            if (forceSelectDependantFeatures)
            {
                foreach (var otherFeature in features)
                {
                    if (otherFeature.DependsOn.Contains(feature))
                    {
                        UnselectFeature(otherFeature);
                    }
                }
            }

            feature.Selected = false;
        }

        private void SelectFeature(Feature feature)
        {
            var featureIndex = GetFeatureItemIndex(feature);
            if (featureIndex > -1)
            {
                checkedListBox1.SetItemChecked(featureIndex, true);
                feature.Selected = true;
            }
            else
            {
                throw new ApplicationException($"Cannot find index of feature '{feature}'");
            }
        }

        public void UnselectFeature(Feature feature)
        {
            var featureIndex = GetFeatureItemIndex(feature);
            if (featureIndex > -1)
            {
                checkedListBox1.SetItemChecked(featureIndex, false);
                feature.Selected = false;
            }
            else
            {
                throw new ApplicationException($"Cannot find index of feature '{feature}'");
            }
        }

        private int GetFeatureItemIndex(Feature feature)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.Items[i] == feature)
                {
                    return i;
                }
            }

            return -1;
        }

        private void UpdateRequiredSpaceOnDisk()
        {
            var size = 0;
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                var feature = checkedListBox1.Items[i] as Feature;
                if (feature.Selected)
                {
                    size += feature.ApproximateSizeMB;
                }
            }

            if (size > 0)
            {
                var requiredSpace = size;
                var availableSpace = Utilis.GetTotalDriveFreeSpace("C:\\Windows") / 1024 / 1024;

                lblRequiredSpaceOnDisk.Text =
                    "Required disk space: " +
                    Utilis.FormatSizeString(size) +
                    Environment.NewLine +
                    "Available disk space: " +
                    Utilis.FormatSizeString(availableSpace);

                if (availableSpace <= requiredSpace)
                {
                    lblRequiredSpaceOnDisk.ForeColor = Color.Red;
                    //avoid navigating to the next page
                }
                else
                {
                    lblRequiredSpaceOnDisk.ForeColor = Color.Black;
                }
            }
            else
            {
                lblRequiredSpaceOnDisk.Text = string.Empty;
            }
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var itemIndex = checkedListBox1.SelectedIndex;

            if (itemIndex > -1)
            {
                var item = checkedListBox1.Items[itemIndex];
                var feature = item as Feature;

                lblFeatureDescription.Text = $"{feature.Description} ({Utilis.FormatSizeString(feature.ApproximateSizeMB)})";
            }
        }

        private void chkAllowFreeSelection_CheckedChanged(object sender, EventArgs e)
        {
            this.forceSelectDependantFeatures = !chkAllowFreeSelection.Checked;
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            foreach (var otherFeature in features)
            {
                SelectFeature(otherFeature);
            }
        }

        private void btnUnselectAll_Click(object sender, EventArgs e)
        {
            foreach (var otherFeature in features)
            {
                UnselectFeature(otherFeature);
            }
        }
    }
}
