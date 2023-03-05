using System;

namespace Scripts
{
    [Serializable]
    public class BusinessID
    {
        public string Value;

        public BusinessID(string id)
        {
            Value = id;
        }

        public override bool Equals(object obj)
        {
            var item = obj as BusinessID;

            if (item == null)
                return false;

            return item.Value.Equals(Value);
        }

        public override int GetHashCode() =>
            Value.GetHashCode();

        public static bool operator ==(BusinessID left, BusinessID right)
        {
            if (left is null)
                return right is null;

            return left.Equals(right);
        }

        public static bool operator !=(BusinessID left, BusinessID right) =>
            !(left == right);
    }
}