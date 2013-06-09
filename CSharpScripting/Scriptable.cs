using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpScripting.Scripting
{
    /// <summary>
    /// An object that is scriptable
    /// </summary>
    public abstract class Scriptable
    {
        /// <summary>
        /// All of the "Script" objects
        /// </summary>
        public Script[] Scripts { get; private set; }

        #region Setup
        /// <summary>
        /// Create a new scriptable object
        /// </summary>
        /// <param name="ScriptAmount">The amount of Scripts total. Use "SetScript()" to set scripts</param>
        public Scriptable(int ScriptAmount)
        {
            this.Scripts = new Script[ScriptAmount];
        }

        /// <summary>
        /// Create a new scriptable object
        /// </summary>
        /// <param name="Scripts">An array of scripts to give the object</param>
        /// <param name="parameters">Parameters to give the scripts for their "Start()" method</param>
        public Scriptable(Script[] Scripts, object[][] parameters = null)
        {
            this.Scripts = Scripts;
            for (int i = 0; i < Scripts.Length; i++)
                if (parameters != null)
                    Scripts[i].Start(parameters[i]);
                else
                    Scripts[i].Start(null);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Gets the value from a member of a script
        /// </summary>
        /// <param name="index">The Script's location in the array "Scripts"</param>
        /// <param name="memberName">Name of the member, as a string</param>
        /// <returns>The value of the member</returns>
        public object GetScriptMemberValue(int index, string memberName)
        {
            return this.Scripts[index].GetMemberValue(memberName);
        }

        /// <summary>
        /// Sets the value from a member of a script
        /// </summary>
        /// <param name="index">The Script's location in the array "Scripts"</param>
        /// <param name="memberName">Name of the member, as a string</param>
        /// <param name="value">The value you are giving the member</param>
        public void SetScriptMemberValue(int index, string memberName, object value)
        {
            this.Scripts[index].SetMemberValue(memberName, value);
        }

        /// <summary>
        /// Invokes a method from a script
        /// </summary>
        /// <param name="index">The Script's location in the array "Scripts"</param>
        /// <param name="MethodName">Name of the method, as a string</param>
        /// <param name="parameters">Parameters to give the method</param>
        public void InvokeScriptMethod(int index, string MethodName, object[] parameters = null)
        {
            this.Scripts[index].InvokeMethod(MethodName, parameters);
        }


        /// <summary>
        /// Gets a script from a certain point
        /// </summary>
        /// <param name="index">The Script's location in the array "Scripts"</param>
        /// <returns>A Script object from "Scripts[index]"</returns>
        public Script GetScript(int index)
        {
            return this.Scripts[index];
        }
        /// <summary>
        /// Set a script at a certain point
        /// </summary>
        /// <param name="index">The Script's location in the array "Scripts"</param>
        /// <param name="script">The script you wish to set</param>
        /// <param name="parameters">Any parameters you wish to give for the Scripts "Start" method</param>
        public void SetScript(int index, Script script, object[] parameters = null)
        {
            this.Scripts[index] = script;
            this.Scripts[index].Start(parameters);
        }

        /// <summary>
        /// Runs all of the scripts in the array "Scripts"
        /// </summary>
        /// <param name="parameters">A 2D array of parameters for each "Run" method</param>
        public void RunAllScripts(object[][] parameters = null)
        {
            for (int i = 0; i < Scripts.Length; i++)
                Scripts[i].Run(parameters[i]);
        }
        /// <summary>
        /// Run a script
        /// </summary>
        /// <param name="index">The Script's location in the array "Scripts"</param>
        /// <param name="parameters">Parameters for the "Run" method</param>
        public void RunScript(int index, object[] parameters = null)
        {
            Scripts[index].Run(parameters);
        }
        #endregion
    }
}
