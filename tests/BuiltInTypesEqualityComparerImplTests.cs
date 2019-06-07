using ObjectFields;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
  [TestClass]
  public class BuiltInTypesEqualityComparerImplTests
  {
    private BuiltInTypesEqualityComparer compare;

    [TestInitialize]
    public void setUp()
    {
      this.compare = new BuiltInTypesEqualityComparerImpl();
    }

    [TestMethod]
    public void should_return_true()
    {
      Assert.IsTrue(this.compare.areEquals(1, 1));
      Assert.IsTrue(this.compare.areEquals('1', '1'));
      Assert.IsTrue(this.compare.areEquals(true, true));
      Assert.IsTrue(this.compare.areEquals("jack", "jack"));
      Assert.IsTrue(this.compare.areEquals(1.50, 1.50));
    }

    [TestMethod]
    public void should_return_false()
    {
      Assert.IsFalse(this.compare.areEquals(null, 1));
      Assert.IsFalse(this.compare.areEquals(null, '1'));
      Assert.IsFalse(this.compare.areEquals(null, "jack"));
      Assert.IsFalse(this.compare.areEquals(null, 1.35));
      Assert.IsFalse(this.compare.areEquals(null, true));
      Assert.IsFalse(this.compare.areEquals(null, null));

      Assert.IsFalse(this.compare.areEquals(true, null));
      Assert.IsFalse(this.compare.areEquals(1, null));
      Assert.IsFalse(this.compare.areEquals(1.28, null));
      Assert.IsFalse(this.compare.areEquals('1', null));
      Assert.IsFalse(this.compare.areEquals("jack", null));
      Assert.IsFalse(this.compare.areEquals("jack", 1));
      Assert.IsFalse(this.compare.areEquals("jack", "john"));
      Assert.IsFalse(this.compare.areEquals("jack", false));
      Assert.IsFalse(this.compare.areEquals("jack", 1.33));
    }
  }
}
