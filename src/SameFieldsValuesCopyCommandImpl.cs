using System;
using System.Reflection;

namespace ObjectFields
{
  public class SameFieldsValuesCopyCommandImpl : SameFieldsValuesCopyCommand
  {
    private BuiltInTypesEqualityComparer compare;
    public SameFieldsValuesCopyCommandImpl()
    {
      this.compare = new BuiltInTypesEqualityComparerImpl();
    }

    private object dst;
    public void setDestination(object value)
    {
      if (value == null)
        throw new ArgumentNullException("value");

      this.dst = value;
    }

    private object src;
    public void setSource(object value)
    {
      if (value == null)
        throw new ArgumentNullException("value");

      this.src = value;
    }

    public void execute()
    {
      this.validateSource();
      this.validateDestination();

      foreach (PropertyInfo srcProp in this.src.GetType().GetProperties())
      {
        PropertyInfo dstProp = 
          this.dst.GetType().GetProperty(srcProp.Name, srcProp.PropertyType);

        if (this.isNullOrReadOnly(dstProp))
          continue;

        object dstValue = dstProp.GetValue(this.dst);
        object srcValue = srcProp.GetValue(this.src);

        if (dstValue == null && srcValue == null)
          continue;

        if (this.compare.areEquals(dstValue, srcValue))
          continue;

        dstProp.SetValue(this.dst, srcValue);
      }
    }

    private void validateSource()
    {
      if (this.src == null)
        throw new InvalidOperationException("Call setSource(object value) first!");
    }

    private void validateDestination()
    {
      if (this.dst == null)
        throw new InvalidOperationException("Call setDestination(object value) first!");
    }

    public bool isNullOrReadOnly(PropertyInfo property)
    {
      if (property == null)
        return true;

      if (!property.CanWrite)
        return true;

      return false;
    }
  }
}
