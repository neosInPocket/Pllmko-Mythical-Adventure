public class Spike : SpawnBase
{
	protected override void InitializePull()
	{

	}

	public override void Destroy()
	{
		gameObject.SetActive(false);
	}
}
