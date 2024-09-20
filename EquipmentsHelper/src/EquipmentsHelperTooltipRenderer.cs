using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.GameContent;

public class EquipmentsHelperTooltipRenderer : IRenderer
{
    private readonly ICoreClientAPI capi;
    private int textureId;
    private bool textureLoaded;

    public double RenderOrder => 0.5;
    public int RenderRange => int.MaxValue;

    public EquipmentsHelperTooltipRenderer(ICoreClientAPI capi)
    {
        this.capi = capi;
        textureLoaded = false;
    }

    public void OnRenderFrame(float deltaTime, EnumRenderStage stage)
    {
        if (stage == EnumRenderStage.Opaque)
        {
            var slot = GetHoveredItemSlot();
            if (slot == null || slot.Itemstack == null) return;

            if (slot.Itemstack.Collectible is ItemWearable)
            {
                string itemText = slot.Itemstack.Item.Code.Path;
                string imageName = DetermineImageBasedOnText(itemText);

                if (!string.IsNullOrEmpty(imageName))
                {
                    RenderImage(imageName);
                }
            }
        }
    }

    private ItemSlot GetHoveredItemSlot()
    {
        return capi.World.Player?.InventoryManager?.CurrentHoveredSlot;
    }

    private string DetermineImageBasedOnText(string text)
    {
        if (text.Contains("armor"))
        {
            return "equipmentshelper_dialog.png";
        }
        return null;
    }

    private void RenderImage(string imageName)
    {
        if (!textureLoaded)
        {
            textureId = capi.Render.GetOrLoadTexture(new AssetLocation("equipmentshelper", "textures/gui/" + imageName));
            textureLoaded = true;
        }

        // Defina as coordenadas de renderização diretamente
        int x = 10;
        int y = 10;
        int width = 128;
        int height = 128;

        capi.Render.Render2DTexturePremultipliedAlpha(textureId, x, y, width, height);
    }

    public void Dispose()
    {
        // Limpar recursos se necessário
    }
}
