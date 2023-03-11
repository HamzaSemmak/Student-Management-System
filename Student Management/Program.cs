using Guna.UI2;
using log4net;
using Student_Management.IHM_s;
using Student_Management.IHM_s.ComposentsGraphique;
using Student_Management.IHM_s.ComposentsGraphique.Formers;
using Student_Management.Modules.LoggerManager;
using Student_Management.Modules.UserModel.Controller;
using Student_Management.Modules.UserModel.Model;
using System;
using System.Security.Cryptography;
using System.Text;
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

        public static void LaunchHandleExceptionForm(string message)
        {
            HandleExceptionForm _HandleExceptionForm = new HandleExceptionForm(message);
            _HandleExceptionForm.Show();
        }

        public static void LaunchFormerForm()
        {
            FormersForm _Formers = new FormersForm();
            _Formers.Show();
        }

        public static void LaunchConfirmExceptionForm(string message)
        {
            ConfirmExceptionForm _ConfirmExceptionForm = new ConfirmExceptionForm(message);
            _ConfirmExceptionForm.Show();
        }

        public static string CurrentDate()
        {
            return $"{dateTime.DayOfWeek}, {dateTime.Day} {dateTime.ToString("MMMM")} {dateTime.Year}";
        }

        public static string GenerateMatricule(int size)
        {
            char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();

            byte[] data = new byte[4 * size];
            using (var crypto = RandomNumberGenerator.Create())
            {
                crypto.GetBytes(data);
            }
            StringBuilder result = new StringBuilder(size);
            for (int i = 0; i < size; i++)
            {
                var rnd = BitConverter.ToUInt32(data, i * 4);
                var idx = rnd % chars.Length;

                result.Append(chars[idx]);
            }
            return result.ToString();
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
