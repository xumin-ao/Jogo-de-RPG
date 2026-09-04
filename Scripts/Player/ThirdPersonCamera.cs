using Godot;

public partial class ThirdPersonCamera : SpringArm3D
{
    [Export] public float MouseSensitivity { get; set; } = 0.01f;
    [Export] public float MinPitch { get; set; } = -0.8f;
    [Export] public float MaxPitch { get; set; } = 0.5f;

    private float _yaw;
    private float _pitch = -0.15f;

    public override void _Ready()
    {
        Input.MouseMode = Input.MouseModeEnum.Captured;
        Rotation = new Vector3(_pitch, _yaw, 0.0f);
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventMouseMotion mouseMotion && Input.MouseMode == Input.MouseModeEnum.Captured)
        {
            _yaw -= mouseMotion.Relative.X * MouseSensitivity;
            _pitch -= mouseMotion.Relative.Y * MouseSensitivity;
            _pitch = Mathf.Clamp(_pitch, MinPitch, MaxPitch);
            Rotation = new Vector3(_pitch, _yaw, 0.0f);
        }

        if (@event is InputEventKey keyEvent && keyEvent.Pressed && keyEvent.Keycode == Key.Escape)
        {
            Input.MouseMode = Input.MouseMode == Input.MouseModeEnum.Captured
                ? Input.MouseModeEnum.Visible
                : Input.MouseModeEnum.Captured;
        }
    }
}
