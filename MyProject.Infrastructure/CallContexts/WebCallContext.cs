using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyProject.Infrastructure
{
    public static class WebCallContext
    {
        private const string DictName = "dict";

        public static object GetData(string name)
        {
            WcfOperationContext wcfContext = WcfOperationContext.Current;
            HttpContext httpContext = HttpContext.Current;

            if (wcfContext != null)
            {
                if (wcfContext.Items.ContainsKey(name))
                {
                    return wcfContext.Items[name];
                }

                return null;
            }

            if (httpContext != null)
            {
                return httpContext.Items[name];
            }

            var skyDict = (Dictionary<string, object>)CallContext.GetData(DictName);
            if (skyDict == null)
            {
                return null;
            }

            lock (skyDict)
            {
                object o;
                if (skyDict.TryGetValue(name, out o))
                {
                    return o;
                }
            }

            return null;
        }

        public static void SetData(string name, object value)
        {
            WcfOperationContext wcfContext = WcfOperationContext.Current;
            HttpContext httpContext = HttpContext.Current;

            if (wcfContext != null)
            {
                wcfContext.Items[name] = value;
            }
            else if (httpContext != null)
            {
                httpContext.Items[name] = value;
            }
            else
            {
                var skyDict = (Dictionary<string, object>)CallContext.GetData(DictName);
                if (skyDict == null)
                {
                    skyDict = new Dictionary<string, object>();
                    CallContext.SetData(DictName, skyDict);
                }

                lock (skyDict)
                {
                    skyDict[name] = value;
                }
            }
        }

        /// <summary>
        /// Remove an item from the call context
        /// </summary>
        /// <param name="name">key of the item that must be removed</param>
        /// <param name="dispose">true (default) disposes an item if it has the IDisposable interface</param>
        /// <remarks>Set dispose to false when the item is this and calling from the Dispose method</remarks>
        public static void FreeNamedDataSlot(string name, bool dispose = true)
        {
            WcfOperationContext wcfContext = WcfOperationContext.Current;
            HttpContext httpContext = HttpContext.Current;

            object itemToFree = GetData(name);
            if (itemToFree != null)
            {
                if (wcfContext != null)
                {
                    wcfContext.Items.Remove(name);
                }
                else if (httpContext != null)
                {
                    httpContext.Items.Remove(name);
                }
                else
                {
                    var skyDict = (Dictionary<string, object>)CallContext.GetData(DictName);
                    lock (skyDict)
                    {
                        skyDict.Remove(name);
                    }
                }
            }

            // Dispose disposable items.
            if (dispose)
            {
                var disposableItem = itemToFree as IDisposable;
                if (disposableItem != null)
                {
                    disposableItem.Dispose();
                }
            }
        }

        public static void FreeNamedDataSlot()
        {
            WcfOperationContext wcfContext = WcfOperationContext.Current;
            HttpContext httpContext = HttpContext.Current;

            // Removing keys while iterating over a dictionary throws an exception, so first put all keys in a list
            List<string> keys;
            if (wcfContext != null)
            {
                keys = wcfContext.Items.Keys.ToList();
            }
            else if (httpContext != null)
            {
                keys = httpContext.Items.Keys.Cast<string>().ToList();
            }
            else
            {
                var skyDict = (Dictionary<string, object>)CallContext.GetData(DictName);
                lock (skyDict)
                {
                    keys = skyDict.Keys.ToList();
                }
            }

            foreach (var key in keys)
            {
                FreeNamedDataSlot(key);
            }
        }
    }
}
