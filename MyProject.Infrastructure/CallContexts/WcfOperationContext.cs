using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Infrastructure
{
    public class WcfOperationContext : IExtension<OperationContext>
    {
        private readonly IDictionary<string, object> _items;

        private WcfOperationContext()
        {
            _items = new Dictionary<string, object>();
        }

        public static WcfOperationContext Current
        {
            get
            {
                if (OperationContext.Current != null)
                {
                    var context = OperationContext.Current.Extensions.Find<WcfOperationContext>();
                    if (context == null)
                    {
                        context = new WcfOperationContext();
                        OperationContext.Current.Extensions.Add(context);
                    }

                    return context;
                }

                return null;
            }
        }

        public IDictionary<string, object> Items
        {
            get { return _items; }
        }

        public void Attach(OperationContext owner) { }

        public void Detach(OperationContext owner) { }
    }
}
