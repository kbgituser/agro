using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlatF.Helpers{
    public static class EnumExtHelper{

        ///// <summary>
        ///// Возвращает описание из DescriptionAttribute примененного к элементу перечисления
        ///// </summary>
        ///// <param name="enumObj">Перечисление</param>
        ///// <returns>Description</returns>
        //public static string GetDescription2(this Enum enumObj){
        //    var fieldInfo = enumObj.GetType().GetField(enumObj.ToString());

        //    var attribArray = fieldInfo.GetCustomAttributes(false);

        //    if (attribArray.Length == 0){
        //        return enumObj.ToString();
        //    }
        //    var attrib = attribArray[0] as DescriptionAttribute;
        //    return attrib != null ? attrib.Description : enumObj.ToString();
        //}

        /// <summary>
        /// Возвращает все значения элемента перечисления
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> GetValues<T>(){
            return Enum.GetValues(typeof(T)).Cast<T>();
        }

        /// <summary>
        /// Возвращает DisplayName из ресурсов по указанному значению Перечисления
        /// </summary>
        /// <param name="obj">Значение Enum </param>
        /// <returns>Значние, иначе NULL</returns>
        public static string GetDisplayName(this Enum obj){
            var type = obj.GetType();
            var fields = type.GetFields();

            foreach (var field in fields){
                var value = field.GetValue(obj);
                if (value.Equals(obj)){
                    var attrs = field.GetCustomAttributes(typeof(DisplayAttribute), true);
                    if (attrs != null && attrs.Length > 0)
                        if (attrs[0] is DisplayAttribute)
                            return ((DisplayAttribute)attrs[0]).GetName();
                }
            }
            return null;
        }


        /// <summary>
        /// Возвращает DisplayName из ресурсов по указанному значению Перечисления
        /// </summary>
        /// <param name="obj">Значение Enum </param>
        /// <returns>Значние, иначе NULL</returns>
        public static string DisplayName(this Enum obj){
            return GetDisplayName(obj);
        }

        /// <summary>
        /// Возвращает коллекцию "Значение" - "Имя для отображения" указанного перечисления 
        /// </summary>
        /// <returns>Возвращает коллекцию ключ значния, Гарантировано не null</returns>
        public static IList<KeyValuePair<int,string>> GetDisplayNameEnumListOld<TEnum>() {
			var result = new List<KeyValuePair<int, string>>();
			if (typeof (TEnum).IsEnum) {
				var fields = typeof (TEnum).GetFields();
				for (int i=1; i < fields.Length; i++) {
					var value = (int)fields[i].GetValue(null);
					string displayName = null;
					var attrs = fields[i].GetCustomAttributes(true);
					if (attrs != null && attrs.Length > 0)
						if (attrs[0] is DisplayAttribute)
							displayName = ((DisplayAttribute)attrs[0]).GetName();
					if (!string.IsNullOrEmpty(displayName))
						result.Add(new KeyValuePair<int, string>(value, displayName));
				}
			}
			return result;
		}
        
        public static IList<KeyValuePair<int,string>> GetDisplayNameEnumList<TEnum>() {
			var result = new List<KeyValuePair<int, string>>();
			if (!typeof(TEnum).IsEnum) 
				return result;
			var list = typeof(TEnum).GetEnumValues().Cast<TEnum>();

			foreach (var item in list)
			{
				var enumType = typeof(TEnum);
				var memberInfos = enumType.GetMember(item.ToString());
				var enumValueMemberInfo = memberInfos.FirstOrDefault(m => m.DeclaringType == enumType);
				if (enumValueMemberInfo == null) 
					continue;
				var valueAttributes = 
					enumValueMemberInfo.GetCustomAttributes(typeof(DisplayAttribute), false);	
				if(valueAttributes.Length < 1)
					continue;
				var displayName = ((DisplayAttribute)valueAttributes[0]).GetName();

				if (!string.IsNullOrEmpty(displayName))
				{
					result.Add(new KeyValuePair<int, string>(Convert.ToInt32(item), displayName));
				}
			}
			return result;
		}

        
		/// <summary>
		/// Возвращает первый попавшийся атрибут
		/// </summary>
		/// <param name="type">Тип</param>
		/// <returns>Если нет атрибута то null</returns>
		public static DisplayAttribute GetAttribute(Type type) {
			var attrs = type.GetCustomAttributes(true);
			foreach (var attr in attrs) {
				if (attr is DisplayAttribute)
					return attr as DisplayAttribute;
			}
			return null;
		}


		public static object GetAttribute(Type type, Type attributeType) {
			var attrs = type.GetCustomAttributes(attributeType, true);
			return attrs[0];
		}


	    public static List<T> GetEnumList<T>() {
		    T[] array = (T[])Enum.GetValues(typeof(T));
		    List<T> list = new List<T>(array);
		    return list;
	    }

	    public static string GetDescription<T>(this T e) where T : IConvertible {
		    if (e is Enum) {
			    var type = e.GetType();
			    var values = Enum.GetValues(type);

			    foreach (int val in values) {
				    if (val == e.ToInt32(CultureInfo.InvariantCulture)) {
					    var memInfo = type.GetMember(type.GetEnumName(val));
					    var descriptionAttribute = memInfo[0]
						    .GetCustomAttributes(typeof(DescriptionAttribute), false)
						    .FirstOrDefault() as DescriptionAttribute;

					    if (descriptionAttribute != null) {
						    return descriptionAttribute.Description;
					    }
				    }
			    }
		    }

		    return string.Empty;
	    }

		public static string GetEnumDescription(this Enum value)
		{
			FieldInfo fi = value.GetType().GetField(value.ToString());

			DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

			if (attributes != null && attributes.Any())
			{
				return attributes.First().Description;
			}

			return value.ToString();
		}

		public static string DescriptionAttr<T>(this T source)
		{
			FieldInfo fi = source.GetType().GetField(source.ToString());

			DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
				typeof(DescriptionAttribute), false);

			if (attributes != null && attributes.Length > 0) return attributes[0].Description;
			else return source.ToString();
		}
	}

}
