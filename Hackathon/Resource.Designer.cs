﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Hackathon {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Hackathon.Resource", typeof(Resource).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;tr&gt;
        ///            &lt;td&gt;{date}&lt;/td&gt;&lt;td&gt;{hours}&lt;/td&gt;
        ///            &lt;td&gt;{intervals}&lt;/td&gt;
        ///           &lt;td&gt;{intervalsDuration}&lt;/td&gt;
        ///            &lt;td&gt;{firstCheck}&lt;/td&gt;
        ///            &lt;td&gt;{lasctCheck}&lt;/td&gt;
        ///        &lt;/tr&gt;.
        /// </summary>
        public static string ItemData {
            get {
                return ResourceManager.GetString("ItemData", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;!DOCTYPE html&gt;
        ///&lt;html lang=&quot;pt-br&quot;&gt;
        ///&lt;head&gt;
        ///&lt;meta charset=&quot;UTF-8&quot;&gt;
        ///&lt;title&gt;Relatório Mensal&lt;/title&gt;
        ///&lt;style&gt;
        ///    body { font-family: Arial, sans-serif; margin: 20px; }
        ///    table { width: 100%; border-collapse: collapse; }
        ///    th, td { text-align: left; padding: 8px; border-bottom: 1px solid #ddd; }
        ///    th { background-color: #f2f2f2; }
        ///    .total { font-weight: bold; }
        ///&lt;/style&gt;
        ///&lt;/head&gt;
        ///&lt;body&gt;
        ///
        ///&lt;h2&gt;Relatório Mensal de Ponto ({month})&lt;/h2&gt;
        ///
        ///&lt;table&gt;
        ///    &lt;thead&gt;
        ///        &lt;tr&gt;
        ///            &lt;th&gt;Dat [rest of string was truncated]&quot;;.
        /// </summary>
        public static string TemplateEmail {
            get {
                return ResourceManager.GetString("TemplateEmail", resourceCulture);
            }
        }
    }
}
