using Godot;

public partial class PlayerController : CharacterBody3D
{
    [Export] public float MoveSpeed { get; set; } = 5.0f;
    [Export] public float Acceleration { get; set; } = 18.0f;
    [Export] public float RotationSpeed { get; set; } = 10.0f;
    [Export] public float Gravity { get; set; } = 18.0f;

    public override void _PhysicsProcess(double delta)
    {
        float dt = (float)delta;

        Vector2 input = Input.GetVector("move_left", "move_right", "move_forward", "move_backward");
        Vector3 direction = new Vector3(input.X, 0.0f, input.Y);

        if (direction.LengthSquared() > 1.0f)
        {
            direction = direction.Normalized();
        }

        Vector3 targetVelocity = direction * MoveSpeed;
        Velocity = new Vector3(
            Mathf.MoveToward(Velocity.X, targetVelocity.X, Acceleration * dt),
            Velocity.Y,
            Mathf.MoveToward(Velocity.Z, targetVelocity.Z, Acceleration * dt)
        );

        if (!IsOnFloor())
        {
            Velocity = new Vector3(Velocity.X, Velocity.Y - Gravity * dt, Velocity.Z);
        }
        else if (Velocity.Y < 0.0f)
        {
            Velocity = new Vector3(Velocity.X, 0.0f, Velocity.Z);
        }

        MoveAndSlide();

        Vector3 horizontalVelocity = new Vector3(Velocity.X, 0.0f, Velocity.Z);
        if (horizontalVelocity.LengthSquared() > 0.01f)
        {
            float targetYaw = Mathf.Atan2(horizontalVelocity.X, horizontalVelocity.Z);
            Rotation = new Vector3(
                Rotation.X,
                Mathf.LerpAngle(Rotation.Y, targetYaw, RotationSpeed * dt),
                Rotation.Z
            );
        }
    }
}
