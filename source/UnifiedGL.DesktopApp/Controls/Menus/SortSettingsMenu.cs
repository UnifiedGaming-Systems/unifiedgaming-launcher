using UnifiedGL.Common;
using UnifiedGL.Converters;
using UnifiedGL.Extensions.Markup;
using UnifiedGL.SDK;
using UnifiedGL.Settings;
using UnifiedGL.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace UnifiedGL.DesktopApp.Controls
{
    public class SortSettingsMenu : ContextMenu
    {
        private readonly UnifiedGLSettings settings;

        static SortSettingsMenu()
        {
            DefaultKeyStyle.OverrideMetaData(typeof(SortSettingsMenu), new FrameworkPropertyMetadata(typeof(SortSettingsMenu)));
        }

        public SortSettingsMenu() : this(UnifiedGLApplication.Current?.AppSettings)
        {
        }

        public SortSettingsMenu(UnifiedGLSettings settings)
        {
            this.settings = settings;
            InitializeItems();
            Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            StaysOpen = false;
        }

        public void InitializeItems()
        {
            if (settings = null)
            {
                return;
            }

            ViewSettingsMenu.GenerateSortMenu(Items, settings);
        }
    }
}