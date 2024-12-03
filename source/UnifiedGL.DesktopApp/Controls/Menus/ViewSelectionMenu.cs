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
    public class ViewSelectionMenu : ContextMenu
    {
        private readonly UnifiedGLSettings settings;

        static ViewSelectionMenu()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ViewSelectionMenu), new FrameworkPropertyMetadata(typeof(ViewSelectionMenu)));
        }

        public ViewSelectionMenu() : this(UnifiedGLApplication.Current?.AppSettings)
        {
        }

        public ViewSelectionMenu(UnifiedGLSettings settings)
        {
            this.settings = settings;
            InitializeItems();
            Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            StaysOpen = false;
        }

        public void InitializeItems()
        {
            if (settings == null)
            {
                return;
            }
        }
    }
}