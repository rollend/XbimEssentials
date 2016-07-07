// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool Xbim.CodeGeneration 
//  
//     Changes to this file may cause incorrect behaviour and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using Xbim.Ifc4.MeasureResource;
using System;
using System.Collections.Generic;
using System.Linq;
using Xbim.Common;
using Xbim.Common.Exceptions;
using Xbim.Ifc4.Interfaces;
using Xbim.Ifc4.PresentationAppearanceResource;
//## Custom using statements
//##

namespace Xbim.Ifc4.Interfaces
{
	/// <summary>
    /// Readonly interface for IfcFillAreaStyle
    /// </summary>
	// ReSharper disable once PartialTypeWithSinglePart
	public partial interface @IIfcFillAreaStyle : IIfcPresentationStyle, IfcPresentationStyleSelect
	{
		IItemSet<IIfcFillStyleSelect> @FillStyles { get; }
		IfcBoolean? @ModelorDraughting { get;  set; }
	
	}
}

namespace Xbim.Ifc4.PresentationAppearanceResource
{
	[ExpressType("IfcFillAreaStyle", 33)]
	// ReSharper disable once PartialTypeWithSinglePart
	public  partial class @IfcFillAreaStyle : IfcPresentationStyle, IInstantiableEntity, IIfcFillAreaStyle, IContainsEntityReferences, IEquatable<@IfcFillAreaStyle>
	{
		#region IIfcFillAreaStyle explicit implementation
		IItemSet<IIfcFillStyleSelect> IIfcFillAreaStyle.FillStyles { 
			get { return new Common.Collections.ProxyItemSet<IfcFillStyleSelect, IIfcFillStyleSelect>( @FillStyles); } 
		}	
		IfcBoolean? IIfcFillAreaStyle.ModelorDraughting { 
 
			get { return @ModelorDraughting; } 
			set { ModelorDraughting = value;}
		}	
		 
		#endregion

		//internal constructor makes sure that objects are not created outside of the model/ assembly controlled area
		internal IfcFillAreaStyle(IModel model, int label, bool activated) : base(model, label, activated)  
		{
			_fillStyles = new ItemSet<IfcFillStyleSelect>( this, 0,  2);
		}

		#region Explicit attribute fields
		private readonly ItemSet<IfcFillStyleSelect> _fillStyles;
		private IfcBoolean? _modelorDraughting;
		#endregion
	
		#region Explicit attribute properties
		[EntityAttribute(2, EntityAttributeState.Mandatory, EntityAttributeType.Set, EntityAttributeType.Class, 1, -1, 2)]
		public IItemSet<IfcFillStyleSelect> @FillStyles 
		{ 
			get 
			{
				if(ActivationStatus != ActivationStatus.NotActivated) return _fillStyles;
				((IPersistEntity)this).Activate(false);
				return _fillStyles;
			} 
		}	
		[EntityAttribute(3, EntityAttributeState.Optional, EntityAttributeType.None, EntityAttributeType.None, -1, -1, 3)]
		public IfcBoolean? @ModelorDraughting 
		{ 
			get 
			{
				if(ActivationStatus != ActivationStatus.NotActivated) return _modelorDraughting;
				((IPersistEntity)this).Activate(false);
				return _modelorDraughting;
			} 
			set
			{
				SetValue( v =>  _modelorDraughting = v, _modelorDraughting, value,  "ModelorDraughting", 3);
			} 
		}	
		#endregion




		#region IPersist implementation
		public override void Parse(int propIndex, IPropertyValue value, int[] nestedIndex)
		{
			switch (propIndex)
			{
				case 0: 
					base.Parse(propIndex, value, nestedIndex); 
					return;
				case 1: 
					_fillStyles.InternalAdd((IfcFillStyleSelect)value.EntityVal);
					return;
				case 2: 
					_modelorDraughting = value.BooleanVal;
					return;
				default:
					throw new XbimParserException(string.Format("Attribute index {0} is out of range for {1}", propIndex + 1, GetType().Name.ToUpper()));
			}
		}
		#endregion

		#region Equality comparers and operators
        public bool Equals(@IfcFillAreaStyle other)
	    {
	        return this == other;
	    }
        #endregion

		#region IContainsEntityReferences
		IEnumerable<IPersistEntity> IContainsEntityReferences.References 
		{
			get 
			{
				foreach(var entity in @FillStyles)
					yield return entity;
			}
		}
		#endregion

		#region Custom code (will survive code regeneration)
		//## Custom code
		//##
		#endregion
	}
}