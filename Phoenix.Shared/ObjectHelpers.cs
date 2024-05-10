namespace Phoenix.Shared
{
    public static class ObjectHelpers
    {
        /// <summary>
        /// Updated the objectBase from the objectModified
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="objectBase"></param>
        /// <param name="objectModified"></param>
        /// <returns></returns>
        public static List<PropertyInfo> UpdateComparibleObject<T>(T objectBase, T objectModified)
        {
            List<string> excProps = new List<string> { "CreateOn", "ChangeCheck" };
            List<PropertyInfo> differences = new List<PropertyInfo>();
            foreach (PropertyInfo property in objectBase!.GetType().GetProperties().Where(x => !excProps.Contains(x.Name)).ToList())
            {
                object? value1 = property.GetValue(objectBase, null);
                object? value2 = property.GetValue(objectModified, null);
                if (value1 != value2)
                {
                    objectBase.GetType().GetProperty(property.Name)?.SetValue(objectBase, value2);
                    differences.Add(property);
                }
            }
            return differences;
        }
    }
}
