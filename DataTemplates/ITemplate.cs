namespace TableForGto.DataTemplates
{
	public interface ITemplate<TParam, TControl>
	{
		public TControl Build(TParam param);
	}
}
