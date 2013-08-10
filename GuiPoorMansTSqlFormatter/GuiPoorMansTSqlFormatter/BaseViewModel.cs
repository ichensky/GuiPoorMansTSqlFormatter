using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GuiPoorMansTSqlFormatter
{
    public abstract class BaseViewModel<TClass> where TClass : BaseViewModel<TClass>
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string name)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(name));
        }

        protected void RaisePropertyChanged(Expression<Func<TClass, object>> expression)
        {
            if (expression == null)
            {
                return;
            }

            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression != null)
            {
                RaisePropertyChanged(memberExpression.Member.Name);
            }
        }
    }
}
