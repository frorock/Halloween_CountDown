﻿#pragma checksum "..\..\..\..\Pages\Game.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "BAA1BB49D697E431791F47D444E597EB4E41E313"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

using Halloween.Pages;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Halloween.Pages {
    
    
    /// <summary>
    /// Game
    /// </summary>
    public partial class Game : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\..\..\Pages\Game.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas SpielCanvas;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\..\Pages\Game.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle Player;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\..\Pages\Game.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label LB_Score;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\..\Pages\Game.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label LB_Life;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\..\Pages\Game.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label LB_Lvl;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\..\Pages\Game.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TB_MScore;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\..\Pages\Game.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TB_Rule;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.8.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Halloween;V1.0.0.0;component/pages/game.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Pages\Game.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.8.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.SpielCanvas = ((System.Windows.Controls.Canvas)(target));
            
            #line 10 "..\..\..\..\Pages\Game.xaml"
            this.SpielCanvas.KeyDown += new System.Windows.Input.KeyEventHandler(this.OnKeyDown);
            
            #line default
            #line hidden
            
            #line 10 "..\..\..\..\Pages\Game.xaml"
            this.SpielCanvas.KeyUp += new System.Windows.Input.KeyEventHandler(this.OnKeyUp);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Player = ((System.Windows.Shapes.Rectangle)(target));
            return;
            case 3:
            this.LB_Score = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.LB_Life = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.LB_Lvl = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.TB_MScore = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 7:
            this.TB_Rule = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

