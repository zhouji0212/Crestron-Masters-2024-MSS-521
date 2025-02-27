//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by CrestronConstruct.
//     Version: 1.3001.21.0
//
//     Project:     MSSXpanel
//     Version:     1.0.0.0
//     Sdk:         CH5:2.7.0
//     Strategy:    Modern
//     IndexOnly:   True
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Crestron.SimplSharpPro.DeviceSupport;
using Crestron.SimplSharpPro;
using MSSXpanel;

namespace MSSXpanel.MainPage
{

    /// <summary>
    /// Allow click events by item
    /// </summary>
    public interface ISourceListByItem
    {
        /// <summary>
        /// Fires on button list presses.  Event carries <see="IndexedButtonEventArgs"/> with ButtonIndex property (0 based).
        /// </summary>
        event EventHandler<IndexedButtonEventArgs> Button_PressEvent;

        /// <summary>
        /// Button1.ItemSelected Feedback
        /// </summary>
        /// <param name="buttonIndex">The index of the button (0 based).</param>
        /// <param name="callback">The delegate to set the feedback.</param>
        void Button_Selected(ushort buttonIndex, SourceListBoolInputSigDelegate callback);

        /// <summary>
        /// Helper Button1.ItemSelected Feedback
        /// </summary>
        /// <param name="buttonIndex">The index of the button (0 based).</param>
        /// <param name="digital">The <see="bool"/> value to set on the panel.</param>
        void Button_Selected(ushort buttonIndex, bool digital);

        /// <summary>
        /// Button1.Text Feedback
        /// </summary>
        /// <param name="buttonIndex">The index of the button (0 based).</param>
        /// <param name="callback">The delegate to set the feedback.</param>
        void Button_Text(ushort buttonIndex, SourceListStringInputSigDelegate callback);

        /// <summary>
        /// Helper Button1.Text Feedback
        /// </summary>
        /// <param name="buttonIndex">The index of the button (0 based).</param>
        /// <param name="serial">The <see cref="string"/> to update the panel.</param>
        void Button_Text(ushort buttonIndex, string serial);

    }


    /// <summary>
    /// Search List
    /// </summary>
    internal partial class SourceList
    {
        #region CH5 Contract

        public event EventHandler<IndexedButtonEventArgs> Button_PressEvent;
        private void onButton_Press(IndexedEventArgs eventArgs)
        {
            EventHandler<IndexedButtonEventArgs> handler = Button_PressEvent;
            if (handler != null)
                handler(this, new IndexedButtonEventArgs((SmartObjectEventArgs)eventArgs.SigArgs, eventArgs.JoinIndex));
        }
                
        /// <summary>
        /// Boolean feedbacks (from Control System to Panel)
        /// </summary>
        /// <param name="buttonIndex">The index of the button (0 based).</param>
        /// <param name="callback">The delegate to set the feedback.</param>
        public void Button_Selected(ushort buttonIndex, SourceListBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.Button_1_Button_SelectedState + buttonIndex], this);
            }
        }

        /// <summary>
        /// Set Boolean feedback (from Control System to Panel)
        /// </summary>
        /// <param name="buttonIndex">The index of the button (0 based).</param>
        /// <param name="digital">The bool value to set.</param>
        public void Button_Selected(ushort buttonIndex, bool digital)
        {
            Button_Selected(buttonIndex, (sig, component) => sig.BoolValue = digital);
        }
        /// <summary>
        /// String feedbacks (from Control System to Panel)
        /// </summary>
        /// <param name="itemIndex">Index of the Widget List (0 based).</param>
        /// <param name="callback">The string delegate to change the panel.</param>
        public void Button_Text(ushort buttonIndex, SourceListStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Button_1_Button_TextState + buttonIndex], this);
            }
        }

        /// <summary>
        /// String feedbacks (from Control System to Panel)
        /// </summary>
        /// <param name="buttonIndex">The index of the button (0 based).</param>
        /// <param name="serial">The <see cref="string"/> to update the panel.</param>
        public void Button_Text(ushort buttonIndex, string serial)
        {
            Button_Text(buttonIndex, (sig, component) => sig.StringValue = serial);
        }

        #endregion
    }

    /// <summary>
    /// SourceList
    /// </summary>
    public interface ISourceList : ISourceListByItem
    {
        object UserObject { get; set; }
    }

    /// <summary>
    /// Digital callback used in feedback events.
    /// </summary>
    /// <param name="boolInputSig">The <see cref="BoolInputSig"/> signal data.</param>
    /// <param name="sourcelist">The <see cref="ISourceList"/> on which to apply the feedback.</param>
    public delegate void SourceListBoolInputSigDelegate(BoolInputSig boolInputSig, ISourceList sourcelist);
    /// <summary>
    /// Digital callback used in feedback events.
    /// </summary>
    /// <param name="stringInputSig">The <see cref="StringInputSig"/> signal data.</param>
    /// <param name="sourcelist">The <see cref="ISourceList"/> on which to apply the feedback.</param>
    public delegate void SourceListStringInputSigDelegate(StringInputSig stringInputSig, ISourceList sourcelist);

    /// <summary>
    /// SourceList
    /// </summary>
    internal partial class SourceList : ISourceList, IDisposable
    {
        #region Standard CH5 Component members

        private ComponentMediator ComponentMediator { get; set; }

        public object UserObject { get; set; }

        /// <summary>
        /// Gets the ControlJoinId a.k.a. SmartObjectId.  This Id identifies the extender symbol.
        /// </summary>
        public uint ControlJoinId { get; private set; }

        private IList<BasicTriListWithSmartObject> _devices;

        /// <summary>
        /// Gets the list of devices.
        /// </summary>
        public IList<BasicTriListWithSmartObject> Devices { get { return _devices; } }

        #endregion

        #region Joins

        private static class Joins
        {
            /// <summary>
            /// Digital signals,
            /// </summary>
            internal static class Booleans
            {
                /// <summary>
                /// Output or Event digital signal from panel to Control System: MainPage.SourceList.Button1ItemPress
                /// Button1.ItemPress
                /// </summary>
                public const uint Button_1_Button_PressEvent = 1001;

                /// <summary>
                /// Output or Event digital signal from panel to Control System: MainPage.SourceList.Button2ItemPress
                /// Button2.ItemPress
                /// </summary>
                public const uint Button_2_Button_PressEvent = 1002;

                /// <summary>
                /// Output or Event digital signal from panel to Control System: MainPage.SourceList.Button3ItemPress
                /// Button3.ItemPress
                /// </summary>
                public const uint Button_3_Button_PressEvent = 1003;

                /// <summary>
                /// Output or Event digital signal from panel to Control System: MainPage.SourceList.Button4ItemPress
                /// Button4.ItemPress
                /// </summary>
                public const uint Button_4_Button_PressEvent = 1004;

                /// <summary>
                /// Output or Event digital signal from panel to Control System: MainPage.SourceList.Button5ItemPress
                /// Button5.ItemPress
                /// </summary>
                public const uint Button_5_Button_PressEvent = 1005;

                /// <summary>
                /// Output or Event digital signal from panel to Control System: MainPage.SourceList.Button6ItemPress
                /// Button6.ItemPress
                /// </summary>
                public const uint Button_6_Button_PressEvent = 1006;


                /// <summary>
                /// Input or Feedback digital signal from Control System to panel: MainPage.SourceList.Button1ItemSelected
                /// Button1.ItemSelected
                /// </summary>
                public const uint Button_1_Button_SelectedState = 1001;

                /// <summary>
                /// Input or Feedback digital signal from Control System to panel: MainPage.SourceList.Button2ItemSelected
                /// Button2.ItemSelected
                /// </summary>
                public const uint Button_2_Button_SelectedState = 1002;

                /// <summary>
                /// Input or Feedback digital signal from Control System to panel: MainPage.SourceList.Button3ItemSelected
                /// Button3.ItemSelected
                /// </summary>
                public const uint Button_3_Button_SelectedState = 1003;

                /// <summary>
                /// Input or Feedback digital signal from Control System to panel: MainPage.SourceList.Button4ItemSelected
                /// Button4.ItemSelected
                /// </summary>
                public const uint Button_4_Button_SelectedState = 1004;

                /// <summary>
                /// Input or Feedback digital signal from Control System to panel: MainPage.SourceList.Button5ItemSelected
                /// Button5.ItemSelected
                /// </summary>
                public const uint Button_5_Button_SelectedState = 1005;

                /// <summary>
                /// Input or Feedback digital signal from Control System to panel: MainPage.SourceList.Button6ItemSelected
                /// Button6.ItemSelected
                /// </summary>
                public const uint Button_6_Button_SelectedState = 1006;

            }
            /// <summary>
            /// Serial signals.
            /// </summary>
            internal static class Strings
            {

                /// <summary>
                /// Input or Feedback serial signal from Control System to panel: MainPage.SourceList.Button1Text
                /// Button1.Text
                /// </summary>
                public const uint Button_1_Button_TextState = 1;
                /// <summary>
                /// Input or Feedback serial signal from Control System to panel: MainPage.SourceList.Button2Text
                /// Button2.Text
                /// </summary>
                public const uint Button_2_Button_TextState = 2;
                /// <summary>
                /// Input or Feedback serial signal from Control System to panel: MainPage.SourceList.Button3Text
                /// Button3.Text
                /// </summary>
                public const uint Button_3_Button_TextState = 3;
                /// <summary>
                /// Input or Feedback serial signal from Control System to panel: MainPage.SourceList.Button4Text
                /// Button4.Text
                /// </summary>
                public const uint Button_4_Button_TextState = 4;
                /// <summary>
                /// Input or Feedback serial signal from Control System to panel: MainPage.SourceList.Button5Text
                /// Button5.Text
                /// </summary>
                public const uint Button_5_Button_TextState = 5;
                /// <summary>
                /// Input or Feedback serial signal from Control System to panel: MainPage.SourceList.Button6Text
                /// Button6.Text
                /// </summary>
                public const uint Button_6_Button_TextState = 6;
            }
        }

        #endregion

        #region Construction and Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="SourceList"/> component class.
        /// </summary>
        /// <param name="componentMediator">The <see cref="ComponentMediator"/> used to instantiate the component.</param>
        /// <param name="controlJoinId">The SmartObjectId at which to create the component.</param>
        /// <param name="itemCount">The number of items.</param>
        internal SourceList(ComponentMediator componentMediator, uint controlJoinId, uint? itemCount)
        {
            ComponentMediator = componentMediator;
            Initialize(controlJoinId, itemCount);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SourceList"/> component class.
        /// </summary>
        /// <param name="componentMediator">The <see cref="ComponentMediator"/> used to instantiate the component.</param>
        /// <param name="controlJoinId">The SmartObjectId at which to create the component.</param>
        internal SourceList(ComponentMediator componentMediator, uint controlJoinId) : this(componentMediator, controlJoinId, null)
        {
        }

        /// <summary>
        /// Initializes a new instance with default itemCount.
        /// </summary>
        /// <param name="controlJoinId">The SmartObjectId at which to create the component.</param>
        private void Initialize(uint controlJoinId)
        {
            Initialize(controlJoinId, null);
        }

        private Dictionary<string, Indexes> _indexLookup = new Dictionary<string, Indexes>();

        /// <summary>
        /// Initializes a new instance of the <see cref="SourceList"/> component class.
        /// </summary>
        /// <param name="controlJoinId">The SmartObjectId at which to create the component.</param>
        /// <param name="itemCount">The number of items.</param>
        private void Initialize(uint controlJoinId, uint? itemCount)
        {
            ControlJoinId = controlJoinId; 
 
            _devices = new List<BasicTriListWithSmartObject>(); 
 
            ComponentMediator.ConfigureBooleanItemEvent(controlJoinId, Joins.Booleans.Button_1_Button_PressEvent, GetIndexes, onButton_Press);
        }

        /// <summary>
        /// Get the offset when using indexed complex components.
        /// </summary>
        /// <param name="controlJoinId">The SmartObjectId of the component.</param>
        /// <param name="join">The join offset.</param>
        /// <param name="eSigType">The join data type.</param>
        private Indexes GetIndexes(uint controlJoinId, uint join, eSigType eSigType)
        {
            if (controlJoinId == ControlJoinId &&
                join >= Joins.Booleans.Button_1_Button_PressEvent &&
                join <= 1006)
            {
                return new Indexes(0, (ushort)(join - Joins.Booleans.Button_1_Button_PressEvent), false);
            }

            return new Indexes(0, 0, true);
        }

        public void AddDevice(BasicTriListWithSmartObject device)
        {
            Devices.Add(device);
            ComponentMediator.HookSmartObjectEvents(device.SmartObjects[ControlJoinId]);
        }

        public void RemoveDevice(BasicTriListWithSmartObject device)
        {
            Devices.Remove(device);
            ComponentMediator.UnHookSmartObjectEvents(device.SmartObjects[ControlJoinId]);
        }

        #endregion

        #region CH5 Contract




        #endregion

        #region Overrides

        public override int GetHashCode()
        {
            return (int)ControlJoinId;
        }

        public override string ToString()
        {
            return string.Format("Contract: {0} Component: {1} HashCode: {2} {3}", "SourceList", GetType().Name, GetHashCode(), UserObject != null ? "UserObject: " + UserObject : null);
        }

        #endregion

        #region IDisposable

        public bool IsDisposed { get; set; }

        public void Dispose()
        {
            if (IsDisposed)
                return;

            IsDisposed = true;

            Button_PressEvent = null;
        }

        #endregion
    }
}
