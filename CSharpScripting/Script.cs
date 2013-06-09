using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace CSharpScripting.Scripting
{
    /// <summary>
    /// An object that is a script
    /// </summary>
    public abstract class Script
    {
        /// <summary>
        /// Start Method, Runs during scripts creation
        /// </summary>
        /// <param name="parameters">Parameters for the method</param>
        public abstract void Start(object[] parameters);
        /// <summary>
        /// Run method, gets called whenevr you want to run the script
        /// </summary>
        /// <param name="parameters">Parameters for the method</param>
        public abstract void Run(object[] parameters);
        

        /// <summary>
        /// Gets the value from a member of the object
        /// </summary>
        /// <param name="memberName">Member's name, as a string</param>
        /// <returns>The value of "memberName"</returns>
        public object GetMemberValue(string memberName)
        {
            var member = this.GetType().GetMember(memberName).Single();
            if (member is PropertyInfo)
                return ((PropertyInfo)member).GetValue(this, null);
            else if (member is FieldInfo)
                return ((FieldInfo)member).GetValue(this);
            else
                throw new ArgumentOutOfRangeException();
        }
        /// <summary>
        /// Sets the value from a member of the object
        /// </summary>
        /// <param name="memberName">Member's name, as a string</param>
        /// <param name="value">"MemberName's" new value</param>
        public void SetMemberValue(string memberName, object value)
        {
            var member = this.GetType().GetMember(memberName).Single();
            if (member is PropertyInfo)
                ((PropertyInfo)member).SetValue(this, value, null);
            else if (member is FieldInfo)
                ((FieldInfo)member).SetValue(this, value);
            else 
                throw new ArgumentOutOfRangeException();
        }
        /// <summary>
        /// Invoke a method from the script
        /// </summary>
        /// <param name="methodName">Name of the method, as a string</param>
        /// <param name="parameters">Parameters to fill into "methodName's" invoke</param>
        public void InvokeMethod(string methodName, object[] parameters)
        {
            this.GetType().GetMethod(methodName).Invoke(this, parameters);
        }
    }
}
