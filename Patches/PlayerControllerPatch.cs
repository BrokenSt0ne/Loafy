using HarmonyLib;
using UnityEngine;

namespace Loafy.Patches
{
    [HarmonyPatch(typeof(PlayerController), "Update")]

    internal class PlayerControllerPatch
    {
        private static bool facingLeft;

        //easier to tell wether you're skipping the method or not i would say this improves readablility 
        private static bool SkipMethod(bool skip) => !skip;

        [HarmonyPrefix]
        private static bool PlayerControllerRewrite(PlayerController __instance, ref bool ___isGrounded, ref bool ___isSprinting, ref Rigidbody2D ___rb)
        {
            if (Plugin.airJumpEnabled)
            {
                ___isGrounded = true;
            }
            else
            {
                ___isGrounded = Physics2D.OverlapCircle(__instance.groundCheck.position, __instance.groundCheckRadius, __instance.whatIsGround);
            }
            float axis = Input.GetAxis("Horizontal");
            if (___isSprinting)
            {
                ___rb.velocity = new Vector2(axis * __instance.sprintSpeed * Plugin.loafScale, ___rb.velocity.y);
            }
            else
            {
                ___rb.velocity = new Vector2(axis * __instance.moveSpeed * Plugin.loafScale, ___rb.velocity.y);
            }

            if (axis > 0f)
            {
                facingLeft = false;
                //__instance.transform.localScale = new Vector3(Plugin.loafScale, Plugin.loafScale, 1f);
            }
            else if (axis < 0f)
            {
                facingLeft = true;
                //__instance.transform.localScale = new Vector3(-Plugin.loafScale, Plugin.loafScale, 1f);
            }

            // this fixes a bug i was having if you're not pressing anything the scale wont change
            if (facingLeft)
            {
                __instance.transform.localScale = new Vector3(-Plugin.loafScale, Plugin.loafScale, 1f);
            }
            else
            {
                __instance.transform.localScale = new Vector3(Plugin.loafScale, Plugin.loafScale, 1f);
            }


            if (Input.GetKeyDown(KeyCode.X) && ___isGrounded)
            {
                ___rb.velocity = new Vector2(___rb.velocity.x, __instance.jumpForce * Plugin.loafScale);
            }

            if (Input.GetKeyDown(KeyCode.Z))
            {
                ___isSprinting = true;
                __instance.moveSpeed = __instance.sprintSpeed * Plugin.loafScale;
            }
            else if (Input.GetKeyUp(KeyCode.Z))
            {
                ___isSprinting = false;
                __instance.moveSpeed = 10f * Plugin.loafScale;
            }

            return SkipMethod(true);
        }
    }
}
