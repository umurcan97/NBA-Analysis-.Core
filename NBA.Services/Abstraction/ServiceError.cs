namespace NBA.Services.Abstraction
{
    using Microsoft.Build.Framework;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class ServiceError
    {
        public ServiceError(string message, int code)
        {
            this.Message = message;
            this.Code = code;
        }

        [Required]
        public string Message { get; set; }

        [Required]
        public int Code { get; }

        #region Errors
        public static ServiceError ForbiddenError => new ServiceError(Resource.ForbiddenError, 997);

        public static ServiceError ModelStateError(string validationError)
        {
            return new ServiceError(validationError, 998);
        } // 998

        public static ServiceError DefaultError => new ServiceError(Resource.DefaultErrorMessage, 999);

        public static ServiceError UserNotFound => new ServiceError(Resource.UserNotFound, 100);
        public static ServiceError FullSeasonNotFound => new ServiceError(Resource.FullSeasonNotFound, 101);
        public static ServiceError FullSeasonQuarterNotFound => new ServiceError(Resource.FullSeasonQuarterNotFound, 102);
        public static ServiceError GameTimeNotFound => new ServiceError(Resource.GameTimeNotFound, 103);
        public static ServiceError PlayerNotFound => new ServiceError(Resource.PlayerNotFound, 104);
        public static ServiceError PlayerStatNotFound => new ServiceError(Resource.PlayerStatNotFound, 105);

        public static ServiceError RequestNotFound => new ServiceError(Resource.RequestNotFound, 121);

        public static ServiceError NotAvailable => new ServiceError(Resource.NotAvailable, 124);

        public static ServiceError SmsNotSend => new ServiceError(Resource.SmsNotSend, 127);

        public static ServiceError UserIsAlreadyRegistered => new ServiceError(Resource.UserIsAlreadyRegistered, 130);

        public static ServiceError WrongSmsCode => new ServiceError(Resource.WrongSmsCode, 132);

        public static ServiceError ErrorConfirmEmail => new ServiceError(Resource.ErrorConfirmEmail, 134);

        public static ServiceError AlreadyConfirmedEmail => new ServiceError(Resource.AlreadyConfirmedEmail, 135);

        public static ServiceError ResetPasswordError => new ServiceError(Resource.ResetPasswordError, 136);

        public static ServiceError InvalidPin => new ServiceError(Resource.InvalidPin, 134);

        public static ServiceError PreviouslyUsedPin => new ServiceError(Resource.PreviouslyUsedPin, 135);

        public static ServiceError SameDigitsPin => new ServiceError(Resource.SameDigitsPin, 137);

        public static ServiceError RolNotFound => new ServiceError("Yetki bulunamadı", 138);

        public static ServiceError RolNotAdd => new ServiceError("Yetki atanamadı", 139);

        public static ServiceError RolAlreadyExists => new ServiceError("Yetki mevcut", 139);

        public static ServiceError RolNotExists => new ServiceError("Yetki kullanıcıda mevcut değil", 140);

        public static ServiceError WrongPassword(int remainingAccessAttempts)
        {
            return new ServiceError(string.Format(Resource.WrongPassword, remainingAccessAttempts), 141);
        }

        public static ServiceError RegisteredPhoneNumber => new ServiceError(Resource.RegisteredPhoneNumber, 142);

        public static ServiceError InvalidCityCode => new ServiceError("Geçersiz şehir kodu", 147);

        public static ServiceError InvalidDistrictCode => new ServiceError("Geçersiz ilçe kodu", 148);

        public static ServiceError UserLockedOut(double remainingMinutes)
        {
            return new ServiceError(string.Format(Resource.UserLockedOut, remainingMinutes), 151);
        } // 151

        public static ServiceError WrongEmail => new ServiceError(Resource.WrongEmail, 152);

        public static ServiceError RolNotRemoved => new ServiceError("Yetkiyi kaldıramazsınız", 158);

        public static ServiceError FeeExist => new ServiceError("Aynı işlem ücreti zaten mevcut.", 160);

        public static ServiceError FeeNotFound => new ServiceError("İşlem ücreti bulunamadı.", 161);

        public static ServiceError PaymentTypeExist => new ServiceError("Aynı ödeme tipi zaten mevcut.", 162);

        public static ServiceError PaymentTypeNotFound => new ServiceError("Ödeme tipi bulunamadı.", 163);

        public static ServiceError VolumeNotFound => new ServiceError("Hacim bulunamadı.", 164);

        public static ServiceError IpNotFound => new ServiceError("IP Adresi bulunamadı.", 165);

        public static ServiceError IpExist => new ServiceError("Aynı IP Adresi zaten mevcut.", 166);

        public static ServiceError YouNeedToSendADifferentEmailAddress => new ServiceError(Resource.YouNeedToSendADifferentEmailAddress, 167);

        public static ServiceError CannotVerifyIdentity => new ServiceError(Resource.CannotVerifyIdentity, 169);

        #endregion


        #region Override Equals Operator

        // Docs: https://msdn.microsoft.com/ru-ru/library/ms173147(v=vs.80).aspx

        public override bool Equals(object obj)
        {
            // If parameter cannot be cast to ServiceError or is null return false.
            var error = obj as ServiceError;

            // Return true if the error codes match. False if the object we're comparing to is nul
            // or if it has a different code.
            return Code == error?.Code;
        }

        public bool Equals(ServiceError error)
        {
            // Return true if the error codes match. False if the object we're comparing to is nul
            // or if it has a different code.
            return Code == error?.Code;
        }

        public override int GetHashCode()
        {
            return Code;
        }

        public static bool operator ==(ServiceError a, ServiceError b)
        {
            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            // Return true if the fields match:
            return a.Equals(b);
        }

        public static bool operator !=(ServiceError a, ServiceError b)
        {
            return !(a == b);
        }

        #endregion


        /// <summary>
        /// ServiceError tipi icerisinde tanimlanmis, hata mesajlarini kodlari ile geri donen methodlari calistirir.
        /// Sonuclarini bir Dictionary olarak geri doner. Bu method, hata kodlari mobil cihazlar icin kodlar onemli oldugu icin hazirlandi.
        /// </summary>
        public static Dictionary<int, string> GetAll()
        {
            var codes = new Dictionary<int, string>();
            void Add(int code, string message)
            {
                if (codes.ContainsKey(code))
                    return;
                codes.Add(code, message);
            }

            var type = typeof(ServiceError);
            var bindingFlags = BindingFlags.Public | BindingFlags.Static;

            var propertyInfos = type.GetProperties(bindingFlags);
            foreach (var propertyInfo in propertyInfos)
            {
                if (propertyInfo.GetIndexParameters().Length > 0)
                    continue;
                if (!propertyInfo.CanRead)
                    continue;
                if (propertyInfo.PropertyType != typeof(ServiceError))
                    continue;
                var returnVal = propertyInfo.GetValue(null);
                if (returnVal == null)
                    continue;
                var serviceError = (ServiceError)returnVal;
                Add(serviceError.Code, serviceError.Message);
            }

            var methodInfos = type.GetMethods(bindingFlags);
            foreach (var methodInfo in methodInfos)
            {
                if (methodInfo.ReturnType != typeof(ServiceError))
                    continue;

                var parameterInfos = methodInfo.GetParameters();
                var parameters = new List<object>();

                foreach (var parameterInfo in parameterInfos)
                {
                    var parameterType = parameterInfo.ParameterType;
                    if (parameterType == typeof(int))
                        parameters.Add(0);
                    else if (parameterType == typeof(decimal))
                        parameters.Add(0m);
                    else if (parameterType == typeof(double))
                        parameters.Add(0d);
                    else if (parameterType == typeof(string))
                        parameters.Add("0");
                    else if (parameterType == typeof(bool))
                        parameters.Add(false);
                }

                var returnVal = methodInfo.Invoke(null, parameters.ToArray());
                if (returnVal == null)
                    continue;
                var serviceError = (ServiceError)returnVal;
                Add(serviceError.Code, serviceError.Message);
            }

            return codes.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
