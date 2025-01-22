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
	public class GroupSettingsMenu : ContextMenu
	{
		private readonly UnifiedGLSettings settings;

		static GroupSettingsMenu()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(GroupSettingsMenu), new FrameworkPropertyMetadata(typeof(GroupSettingsMenu)));
		}
		public GroupSettingsMenu() : this(UnifiedGLApplication.Current?.AppSettings)
		{
		}

		public GroupSettingsMenu(UnifiedGLSettings settings)
		{
			this.settings = settings;
			InitializeItems();
			Placement = System.Windows.Controls.Primatives.PlacementMode.Bottom;
			StaysOpen = false;
		}

		public void InitializeItems()
		{
			if (settings == null)
			{
				return;
			}

			ViewSettingsMenu.GeneratedGroupMenu(Items, settings);
		}
	}
}