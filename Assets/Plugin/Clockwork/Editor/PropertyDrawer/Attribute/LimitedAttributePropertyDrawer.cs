using System;
using UnityEngine;
using UnityEditor;

namespace Clockwork.Editor
{
	[CustomPropertyDrawer(typeof(LimitedAttribute), useForChildren: true)]
	public class LimitedAttributePropertyDrawer : PropertyDrawer
	{
		private Limit m_Limit;

		static private void DrawRangeField(Rect rect, ref Range value, Range limit, GUIContent label)
		{
			const float sliderWidthPercent = 0.8f;
			const float boxWidthPercent    = 0.1f;

			var totalWidth  = rect.width;
			var sliderWidth = totalWidth * sliderWidthPercent;
			var boxWidth    = totalWidth * boxWidthPercent;
			var spaceWidth  = (totalWidth - (sliderWidth + boxWidth * 2f)) / 2f;

			var _rect = rect;

			_rect.width = sliderWidth;
			EditorGUI.MinMaxSlider(
				_rect,
				label,
				ref value.start,
				ref value.end,
				limit.start,
				limit.end);

			_rect.x += sliderWidth + spaceWidth;
			_rect.width = boxWidth;
			value.start = EditorGUI.FloatField(
				_rect,
				value.start);

			_rect.x += boxWidth + spaceWidth;
			value.end = EditorGUI.FloatField(
				_rect,
				value.end);

			value.start = Mathf.Clamp(value.start, limit.start, value.end);
			value.end   = Mathf.Clamp(value.end,   value.start, limit.end);
		}

		protected override void Initialize(SerializedProperty property, GUIContent label)
		{
			FieldType fieldType;
			switch (property.propertyType)
			{
				case SerializedPropertyType.Generic:
					var _fieldType = this.GetPropertyFieldType();
					if (_fieldType == typeof(Range))
					{
						fieldType = FieldType.Range;
						break;
					}
					else goto default;

				case SerializedPropertyType.Integer:
					fieldType = FieldType.Integer;
					break;

				case SerializedPropertyType.Float:
					fieldType = FieldType.Float;
					break;

				default:
					Error = Error.revert;
					return;
			}

			var attr = (LimitedAttribute)attribute;
			m_Limit = new(fieldType, attr.min, attr.max);

			if (Single.IsInfinity(m_Limit.Min) && Single.IsInfinity(m_Limit.Max))
			{
				Error = "Min and Max cannot be infinity";
				return;
			}

			if (Single.IsNaN(m_Limit.Min) || Single.IsNaN(m_Limit.Max))
			{
				Error = "Min or Max cannot be NaN";
				return;
			}

			if (m_Limit.fieldType is FieldType.Range && m_Limit.open)
			{
				Error = "Range cannot be open";
				return;
			}
		}

		protected override void Update(Rect rect, SerializedProperty property, GUIContent label)
		{
			switch (m_Limit.fieldType)
			{
				case FieldType.Float:
					DrawFloatField();
					return;

				case FieldType.Integer:
					DrawIntegerField();
					return;

				case FieldType.Range:
					DrawRangeField();
					return;

				default:
					throw new EnumIndexException(m_Limit.fieldType);
			}

			void DrawFloatField()
			{
				var value = property.floatValue;

				if (m_Limit.open)
				{
					value = EditorGUI.FloatField(
						rect,
						label,
						value);
				}
				else
				{
					value = EditorGUI.Slider(
						rect,
						label,
						value,
						m_Limit.Min,
						m_Limit.Max);
				}

				property.floatValue = m_Limit.range.Limit(value);
			}

			void DrawIntegerField()
			{
				var value = property.intValue;

				if (m_Limit.open)
				{
					value = EditorGUI.IntField(
						rect,
						label,
						value);
				}
				else
				{
					value = EditorGUI.IntSlider(
						rect,
						label,
						value,
						(int)m_Limit.Min,
						(int)m_Limit.Max);
				}

				property.intValue = (int)m_Limit.range.Limit(value);
			}

			void DrawRangeField()
			{
				var startProp = property.GetPropertySafe(nameof(Range.start));
				var endProp = property.GetPropertySafe(nameof(Range.end));
				
				var value = new Range(
					startProp.floatValue,
					endProp.floatValue);

				LimitedAttributePropertyDrawer.DrawRangeField(
					rect,
					ref value,
					m_Limit.range,
					label);

				startProp.floatValue = value.start;
				endProp.floatValue = value.end;
			}

		}

		protected override float GetHeight(SerializedProperty property, GUIContent label)
		{
			return EditorGUIUtility.singleLineHeight;
		}

		private enum FieldType
		{
			Float,
			Integer,
			Range
		}

		private readonly struct Limit
		{
			public readonly FieldType fieldType;
			public readonly Range range;
			public readonly bool open;

			public Limit(FieldType fieldType, float min, float max)
			{
				this.fieldType = fieldType;
				range = new(min, max);
				open = Single.IsInfinity(min) || Single.IsInfinity(max);
			}

			public float Min
			{
				get => range.start;
			}

			public float Max
			{
				get => range.end;
			}

		}

	}
}