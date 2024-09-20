using Vintagestory.API.Client;
using Vintagestory.API.Common;

[assembly: ModInfo("EquipmentsHelper", "equipmentshelper",
    Version = "0.0.2",
    Authors = new[] { "Mintir4" },
    Description = "A mod to help with equipment tooltips")]

namespace EquipmentsHelper
{
    public class EquipmentsHelperMod : ModSystem
    {
        private EquipmentsHelperTooltipRenderer tooltipRenderer;

        public override void StartClientSide(ICoreClientAPI api)
        {
            base.StartClientSide(api);
            tooltipRenderer = new EquipmentsHelperTooltipRenderer(api);

            // Registrar o evento de renderização
            api.Event.RegisterRenderer(tooltipRenderer, EnumRenderStage.Opaque, "equipmentshelpertooltip");
        }

        public override void Dispose()
        {
            tooltipRenderer?.Dispose();
            base.Dispose();
        }
    }
}
