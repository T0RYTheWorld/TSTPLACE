public class FoodData : InstanceTypeData
{
    public float freshness;
    public int portions;

    public override InstanceTypeData Copy()
    {
        return new FoodData
        {
            freshness = this.freshness,
            portions = this.portions
        };
    }
}
