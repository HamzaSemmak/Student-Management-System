﻿using Guna.UI2;
using log4net;
using Student_Management.IHM_s;
using Student_Management.IHM_s.ComposentsGraphique;
using Student_Management.Modules.LoggerManager;
using Student_Management.Modules.UserModel.Controller;
using Student_Management.Modules.UserModel.Model;
using System;
using System.Windows.Forms;
using static log4net.Appender.RollingFileAppender;

namespace Student_Management
{
    internal static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        public static readonly ILog logger = Log4NetManager.GetLogger(typeof(Program));
        public static DateTime dateTime = DateTime.Now;

        public static LoadingForm _Splash()
        {
            return new LoadingForm();
        }

        public static void CloseApplication()
        {
            Application.Exit();
            UsersController User = new UsersController();
            User.forgetUser();
            logger.Info("DisConnect...");
            logger.Info("Exit Application Student Management System");
        }

        public static void LaunchAuthentification()
        {
            Authentification authentification = new Authentification();
            authentification.Show();
        }

        public static void LaunchPrincipalForm()
        {
            PrincipalForm principalForm = new PrincipalForm();
            principalForm.Show();
        }

        public static void LaunchUnhandleExceptionForm(string message)
        {
            UnhandleExceptionForm _UnhandleExceptionForm = new UnhandleExceptionForm(message);
            _UnhandleExceptionForm.Show();
        }

        public static void LaunchFormerForm()
        {
            Formers _Formers = new Formers();
            _Formers.Show();
        }

        public static string CurrentDate()
        {
            return $"{dateTime.DayOfWeek}, {dateTime.Day} {dateTime.ToString("MMMM")} {dateTime.Year}";
        }

        [STAThread]
        static void Main()
        {
            logger.Info("Start Application Student Management System");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(_Splash());
        }
    }
}
