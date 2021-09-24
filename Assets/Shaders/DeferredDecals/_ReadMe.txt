This scene shows a basic implementation of "deferred decals" - i.e.
it shows how command buffers can be used to modify the deferred
shading g-buffer before lighting is done.

Idea behind them is described by Emil Persson here:
http://www.humus.name/index.php?page=3D&ID=83 and Pope Kim here:
http://www.popekim.com/2012/10/siggraph-2012-screen-space-decals-in.html

The idea is: after g-buffer is done, draw each "shape" of the decal (e.g. box)
and modify the g-buffer contents. This is very similar to how lights are done
in deferred shading, except instead of accumulating the lighting
we modify the g-buffer textures.

Note: this is small example code, and not a production-ready decal system!

In this example, each decal should have Decal.cs script attached; transform
placement and scale defines decal area of influence. Decals can be of three kinds:

* Diffuse only (only affects underlying surface's diffuse color)
* Normals only (only affects normals of the underlying surface; does not change color)
* Both diffuse & normals

Each type should use appropriate shader to render its effect, see shaders in DeferredDecals
folder.

DeferredDecalRenderer.cs is a script that should be assigned to some "always visible"
object (e.g. ground). When that object becomes visible by any camera, it will add
command buffers with all the decals to that camera. This way this works for scene view
cameras too, but is not an ideal setup for a proper decal system.

One caveat though: in current Unity's deferred shading implementation, lightmaps, ambient
and reflection probes are done as part of g-buffer rendering pass. Since decals only
modify the g-buffer after it's done, they *do not affect* ambient/lightmaps! This means
that for example in the shadow of a light (where no other lights are shining),
decals will not be visible.
