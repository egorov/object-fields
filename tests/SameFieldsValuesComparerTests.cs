using ObjectFields;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Tests
{
  [TestClass]
  public class SameFieldsValuesComparerTests
  {
    private SameFieldsValuesComparer compare;

    [TestInitialize]
    public void setUp()
    {
      this.compare = new SameFieldsValuesComparerImpl(
        new BuiltInTypesEqualityComparerImpl()
      );
    }

    [TestMethod]
    public void should_return_true()
    {
      Sample standard = new Sample();
      Sample sample = new Sample();
      Another another = new Another();

      Assert.IsTrue(this.compare.areEquals(standard, sample));
      Assert.IsTrue(this.compare.areEquals(sample, another));
      Assert.IsTrue(this.compare.areEquals(standard, another));
    }

    [TestMethod]
    public void should_return_false()
    {
      Sample standard = new Sample() { name = "jack" };
      Sample sample = new Sample();
      Another another = new Another() { name = "john" };

      Assert.IsFalse(this.compare.areEquals(standard, sample));
      Assert.IsFalse(this.compare.areEquals(another, sample));
      Assert.IsFalse(this.compare.areEquals(another, standard));
    }

    [TestMethod]
    public void should_throw()
    {
      Action nullCompare = () => new SameFieldsValuesComparerImpl(null);

      Assert.ThrowsException<ArgumentNullException>(nullCompare);
    }
  }
}
