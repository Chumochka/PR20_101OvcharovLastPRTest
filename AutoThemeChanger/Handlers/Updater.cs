﻿using System;
using System.Reflection;
using System.Xml;
using System.Windows;
using System.Globalization;

namespace AutoThemeChanger
{
    class Updater
    {
        //https://raw.githubusercontent.com/Armin2208/Windows-Auto-Night-Mode/master/version.xml
        string xmlURL = "https://raw.githubusercontent.com/Armin2208/Windows-Auto-Night-Mode/master/version.xml";
        Version newVersion = null;
        readonly Version currentVersion = Assembly.GetExecutingAssembly().GetName().Version;
        string url;
        bool silent;
        bool updateAvailable = false;

        public Updater(bool pSilent)
        {
            this.silent = pSilent;
        }

        public bool IsUpdateAvailable()
        {
            return updateAvailable;
        }

        public string GetUpdateURL()
        {
            return url;
        }

        public void CheckNewVersion()
        {
            XmlTextReader reader = new XmlTextReader(xmlURL);
            reader.MoveToContent();
            string elementName = "AutoNightMode";
            if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "AutoNightMode"))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        elementName = reader.Name;
                    }
                    else
                    {
                        if ((reader.NodeType == XmlNodeType.Text) && (reader.HasValue))
                        {
                            switch (elementName)
                            {
                                case "version":
                                    newVersion = new Version(reader.Value);
                                    break;
                                case "url":
                                    url = reader.Value;
                                    break;
                            }
                        }
                    }
                }
            }
            reader.Close();
            MessageBoxHandler();
        }

        private void MessageBoxHandler()
        {
            CultureInfo.CurrentUICulture = new CultureInfo(Properties.Settings.Default.Language, true);
            if (currentVersion.CompareTo(newVersion) < 0)
            {
                updateAvailable = true;

                if (!silent)
                {
                    if(newVersion.Major > 9)
                    {
                        if(Properties.Settings.Default.WantsVersion10)
                        {
                            Ver10Updater updater = new Ver10Updater(url);
                            updater.Topmost = true;
                            updater.Show();
                            updater.Activate();
                        }
                    }
                    else
                    {
                        string text = String.Format(Properties.Resources.msgUpdaterText, currentVersion, newVersion);
                        MsgBox msgBox = new MsgBox(text, "Auto Dark Mode Updater", "update", "yesno")
                        {
                            WindowStartupLocation = WindowStartupLocation.CenterScreen,
                            Topmost = true
                        };
                        msgBox.ShowDialog();
                        var result = msgBox.DialogResult;
                        if (result == true)
                        {
                            System.Diagnostics.Process.Start(url);
                            Application.Current.Shutdown();
                        }
                    }
                }
            }
        }
    }
}