public class ApplyComponent : ItemInventaryComponent
{
    protected override bool TryAddItemInventary(Inventary inventary)
    => inventary.TryAddApply();
}
