using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace Etsy2ParcelMonkeyCSV.Helper
{
    /// <summary>
    /// Permet d'executer une action asynchrone de façon synchrone
    /// </summary>
    /// <remarks>
    /// Implémentation de Microsoft
    /// assembly Microsoft.AspNet.Identity.Core 2.2.1
    /// </remarks>
    public static class AsyncHelper
    {
        private static readonly TaskFactory TaskFactory = new TaskFactory();

        public static void RunSync(Func<Task> func)
        {
            CultureInfo cultureUi = CultureInfo.CurrentUICulture;
            CultureInfo culture = CultureInfo.CurrentCulture;

            TaskFactory.StartNew<Task>(() =>
            {
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = cultureUi;

                return func();
            }).Unwrap().GetAwaiter().GetResult();
        }

        public static TResult RunSync<TResult>(Func<Task<TResult>> func)
        {
            CultureInfo cultureUi = CultureInfo.CurrentUICulture;
            CultureInfo culture = CultureInfo.CurrentCulture;

            return TaskFactory.StartNew<Task<TResult>>(() =>
            {
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = cultureUi;

                return func();
            }).Unwrap<TResult>().GetAwaiter().GetResult();
        }
    }
}