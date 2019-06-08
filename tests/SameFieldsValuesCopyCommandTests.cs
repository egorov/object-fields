using System;
using System.Reflection;
using ObjectFields;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
  [TestClass]
  public class SameFieldsValuesCopyCommandTests
  {
    private SameFieldsValuesCopyCommand command;

    [TestInitialize]
    public void setUp()
    {
      this.command = new SameFieldsValuesCopyCommandImpl(
        new BuiltInTypesEqualityComparerImpl()
      );
    }

    [TestMethod]
    public void should_copy_all_values()
    {
      Sample record = this.makeSample();
      SampleSnapshot snapshot = new SampleSnapshot();

      this.command.setSource(record);
      this.command.setDestination(snapshot);
      this.command.execute();

      foreach(PropertyInfo srcProperty in record.GetType().GetProperties())
      {
        PropertyInfo dstProperty = 
          snapshot.GetType().GetProperty(srcProperty.Name);

        object srcValue = srcProperty.GetValue(record);
        object dstValue = dstProperty.GetValue(snapshot);

        Assert.AreEqual(srcValue, dstValue);
      }
    }

    private Sample makeSample()
    {
      return new Sample()
      {
        id = 55,
        name = "jack",
        isValid = true,
        gender = Gender.Male,
        age = 20.5M
      };
    }

    [TestMethod]
    public void should_copy_just_same_fields()
    {
      Sample one = this.makeSample();
      Another another = new Another();

      this.command.setSource(one);
      this.command.setDestination(another);
      this.command.execute();

      Assert.AreEqual(another.name, one.name);
      Assert.AreEqual(0, another.Id);
      Assert.IsFalse(another.isEnabled);
    }

    [TestMethod]
    public void should_set_null()
    {
      Sample src = new Sample();
      Sample dst = new Sample() { name = "value" };
      this.command.setSource(src);
      this.command.setDestination(dst);
      this.command.execute();

      Assert.IsNull(dst.name);
      Assert.AreEqual(dst.name, src.name);
    }

    [TestMethod]
    public void should_throw()
    {
      Action setSourceNull = () => this.command.setSource(null);
      Assert.ThrowsException<ArgumentNullException>(setSourceNull);

      Action setDestinationNull = () => this.command.setDestination(null);
      Assert.ThrowsException<ArgumentNullException>(setDestinationNull);
    }

    [TestMethod]
    public void should_throw_if_source_was_not_set()
    {
      this.command.setDestination(new Sample());

      Action sourceWasNotSet = () => this.command.execute();
      Assert.ThrowsException<InvalidOperationException>(sourceWasNotSet);
    }

    [TestMethod]
    public void should_throw_if_destination_was_not_set()
    {
      this.command.setSource(new Sample());

      Action destinationWasNotSet = () => this.command.execute();
      Assert.ThrowsException<InvalidOperationException>(destinationWasNotSet);
    }

    [TestMethod]
    public void should_throw_on_construction()
    {
      Action nullCompare = () => new SameFieldsValuesCopyCommandImpl(null);

      Assert.ThrowsException<ArgumentNullException>(nullCompare);
    }


    [TestMethod]
    public void should_not_throw()
    {
      this.command.setSource("not reference type");
      this.command.setDestination(new Sample());
      this.command.execute();

      this.command.setSource(new Sample());
      this.command.setDestination(34);
      this.command.execute();

      this.command.setSource(true);
      this.command.setDestination("y");
      this.command.execute();
    }
  }
}
