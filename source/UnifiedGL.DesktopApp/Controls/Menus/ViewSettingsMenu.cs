using UnifiedGL.Common;
using UnifiedGL.Converters;
using UnifiedGL.Extensions.Markup;
using UnifiedGL.SDK;
using UnifiedGL.SDK.Models;
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
    public class ViewSettingsMenu : ContextMenu
    {
        private readonly UnifiedGLSettings settings;

        static ViewSettingsMenu()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ViewSettingsMenu), new FrameworkPropertyMetadata(typeof(ViewSettingsMenu)));
        }

        public ViewSettingsMenu() : this(UnifiedGLApplication.Current?.AppSettings)
        {
        }

        public ViewSettingsMenu(UnifiedGLSettings settings)
        {
            this.settings = settings;
            InitializeItems();
            Placement = System.Windows.Controls.Primatives.PlacementMode.Button;
            StaysOpen = false;
        }

        public static void GenerateSortMenu(ItemCollection itemsRoot, UnifiedGLSettings settings)
        {
            MenuHelpers.PopulateEnumOptions<SortOrderDirection>(itemsRoot, nameof(settings.ViewSettings.SortingOrderDirection), settings.ViewSettings);
            itemsRoot.Add(new Separator());
            MenuHelpers.PopulateEnumOptions<SortOrder>(itemsRoot, nameof(settings.ViewSettings.SortOrder), settings.ViewSettings, true);
        }

        public static void GenerateGroupMenu(ItemCollection itemsRoot, UnifiedGLSettings settings)
        {
            var dontGroupItem = MainMenu.AddMenuChild(itemsRoot, GroupableField.None.GetDescription(), null);
            dontGroupItem.IsCheckable = true;
            MenuHelpers.SetEnumBinding(dontGroupItem, nameof(settings.ViewSettings.GroupingOrder), settings.ViewSettings, GroupableField.None);
            itemsRoot.Add(new Separator());
            MenuHelpers.PopulateEnumOptions<GroupableField>(itemsRoot, nameof(settings.ViewSettings.GroupingOrder), settings.ViewSettings, true,
                new List<GroupableField> { GroupableField.None });
        }

        public void InitializeItems()
        {
            if (settings == null)
            {
                return;
            }

            Items.Clear();

            // Sort By
            var sortItem = new MenuItem
            {
                Header = ResourceProvider.GetString("LOCMenuSortByTitle")
            };
            GenerateSortMenu(sortItem.Items, settings);

            // Group By
            var groupItem = new MenuItem
            {
                Header = ResourceProvider.GetString("LOCMenuGroupByTitle")
            };
            GenerateGroupMenu(groupItem.Items, settings);
            Items.Add(sortItem);
            Items.Add(groupItem);
            Items.Add(new Separator());

            // View Type
            MenuHelpers.PopulateEnumOptions<DesktopView>(Items, nameof(settings.ViewSettings.GamesViewType), settings.ViewSettings);
            Items.Add(new Separator());

            // View
            var filterItem = MainMenu.AddMenuChild(Items, "LOCMenuViewFilterPanel", null);
            filterItem.IsCheckable = true;
            BindingOperations.SetBinding(filterItem, MenuItem.IsCheckedProperty,
                new Binding
                {
                    Source = settings,
                    Path = new PropertyPath(nameof(UnifiedGLSettings.FilterPanelVisible))
                });

            var explorerItem = MainMenu.AddMenuChild(Items, "LOCMenuViewExplorerPanel", null);
            explorerItem.IsCheckable = true;
            BindingOperations.SetBinding(explorerItem, MenuItem.IsCheckedProperty,
                new Binding
                {
                    Source = settings,
                    Path = new PropertyPath(nameof(UnifiedGLSettings.ExplorerPanelVisible))
                });
        }
    }
}