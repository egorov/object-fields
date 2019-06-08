using System;
using System.Reflection;

namespace ObjectFields
{
    public class SameFieldsValuesComparerImpl : SameFieldsValuesComparer
  {
    private BuiltInTypesEqualityComparer compare;

    public SameFieldsValuesComparerImpl()
      : this(new BuiltInTypesEqualityComparerImpl()) { }

    public SameFieldsValuesComparerImpl(BuiltInTypesEqualityComparer compare)
    {
      if(compare == null)
        throw new ArgumentNullException(nameof(compare));
      
      this.compare = compare;
    }

    public bool areEquals(object standard, object sample)
    {
      this.validate(standard, "standard");
      this.validate(sample, "sample");

      foreach (PropertyInfo stdProp in standard.GetType().GetProperties())
      {
        PropertyInfo smplProp = 
          sample.GetType().GetProperty(stdProp.Name, stdProp.PropertyType);

        if(smplProp == null)
          continue;

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