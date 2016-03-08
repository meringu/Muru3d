using Microsoft.Xna.Framework;

namespace muru3d
{
    public class Camera
    {
        public float Tilt { get; set; }
        public float Rotation { get; set; }
        public float Zoom { get; set; }

        public Camera()
        {
            Tilt = MathHelper.PiOver2 * 0.45f;
            Rotation = MathHelper.PiOver4;
            Zoom = 2f;
        }

        public Matrix ViewMatrix()
        {
            var cameraPosition = (Matrix.CreateTranslation(Vector3.Forward * Zoom) * Matrix.CreateFromYawPitchRoll(Rotation, Tilt, 0)).Translation;
            return Matrix.CreateLookAt(cameraPosition, Vector3.Zero, Vector3.Up);
        }

        public Matrix ProjectionMatrix(float aspectRatio)
        {
            return Matrix.CreatePerspectiveFieldOfView(MathHelper.Pi / 2f, aspectRatio, Zoom / 10f, Zoom * 100f);
        }
        public void Sanitize()
        {
            var maxTilt = MathHelper.PiOver2 * 0.9f;
            Tilt = MathHelper.Clamp(Tilt, -maxTilt, maxTilt);
            Rotation = MathHelper.WrapAngle(Rotation);
        }
    }
}
