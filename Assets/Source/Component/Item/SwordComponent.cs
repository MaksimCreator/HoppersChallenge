public class SwordComponent : ItemInventaryComponent
{
    protected override bool TryAddItemInventary(Inventary inventary)
    => inventary.TryAddSword();
}