using System;
using System.Reflection;

namespace ObjectFields
{
    public class SameFieldsValuesComparerImpl : SameFieldsValuesComparer
  {
    private BuiltInTypesEqualityComparer compare;

    public SameFieldsValuesComparerImpl()
    {
      this.compare = new BuiltInTypesEqualityComparerImpl();
    }

    public bool areEquals(object standard, object sample)
    {
      this.validate(standard, "standard");
      this.validate(sample, "sample");

      foreach (PropertyInfo stdProp in standard.GetType().GetProperties())
      {
        PropertyInfo smplProp = 
          sample.GetType().GetProperty(stdProp.Name, stdProp.PropertyType);

        object stdValue = stdProp.GetValue(standard);
        object smplValue = smplProp.GetValue(sample);

        if (smplValue == null && stdValue == null)
          return true;

        if (!this.compare.areEquals(smplValue, stdValue))
          return false;
      }
      return true;
    }

    private void validate(object value, string name)
    {
      if (value == null)
        throw new ArgumentNullException(name);
    }
  }
}