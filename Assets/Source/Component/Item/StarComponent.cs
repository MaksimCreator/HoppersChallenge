public class StarComponent : ItemInventaryComponent
{
    protected override bool TryAddItemInventary(Inventary inventary)
    => inventary.TryAddStar();
}
