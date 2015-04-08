using QuickUnity.Utilitys;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The FX namespace.
/// </summary>
namespace QuickUnity.FX
{
    /// <summary>
    /// Simple ocean visual effect.
    /// </summary>
    [AddComponentMenu("QuickUnity/FX/Simple Ocean")]
    public class SimpleOcean : MonoBehaviour
    {
        /// <summary>
        /// The gravity acceleration constant.
        /// </summary>
        private const float GRAVITY_ACCELERATION = 9.81f;

        /// <summary>
        /// The humidity of force storm.
        /// </summary>
        private const float FORCE_STORM_HUMIDITY = 1.0f;

        /// <summary>
        /// The humidit update frequency.
        /// </summary>
        private const float HUMIDITY_UPDATE_FREQUENCY = 1.0f / 280.0f;

        /// <summary>
        /// The maximum LOD.
        /// </summary>
        private const int MAX_LOD = 1;

        /// <summary>
        /// Set true simulate ocean when it can be seen, otherwise set false.
        /// </summary>
        public bool VisibleSimulation;

        /// <summary>
        /// The render layers.
        /// </summary>
        public LayerMask RenderLayers = -1;

        /// <summary>
        /// The count of tiles.
        /// </summary>
        public int TilesCount = 2;

        /// <summary>
        /// The normal map width.
        /// </summary>
        public int NormalMapWidth = 128;

        /// <summary>
        /// The normal map height.
        /// </summary>
        public int NormalMapHeight = 128;

        /// <summary>
        /// The RenderTexture width.
        /// </summary>
        public int RenderTextureWidth = 128;

        /// <summary>
        /// The RenderTexture height.
        /// </summary>
        public int RenderTextureHeight = 128;

        /// <summary>
        /// The width of tile polygon.
        /// </summary>
        public int TilePolygonWidth = 32;

        /// <summary>
        /// The height of tile polygon.
        /// </summary>
        public int TilePolygonHeight = 32;

        /// <summary>
        /// The wave speed.
        /// </summary>
        public float WaveSpeed = 0.7f;

        /// <summary>
        /// The wave scale.
        /// </summary>
        public float WaveScale = 0.1f;

        /// <summary>
        /// The choppy scale.
        /// </summary>
        public float ChoppyScale = 2.0f;

        /// <summary>
        /// The light direction.
        /// </summary>
        [SerializeField]
        private Vector4 mLightDirection;

        /// <summary>
        /// Gets or sets the light direction.
        /// </summary>
        /// <value>The light direction.</value>
        public Vector4 LightDirection
        {
            get { return mLightDirection; }
            set
            {
                if (mLightDirection != value)
                {
                    mLightDirection = value;
                    UpdateLightDirection();
                }
            }
        }

        /// <summary>
        /// The force storm.
        /// </summary>
        public bool ForceStorm;

        /// <summary>
        /// The ocean tile size.
        /// </summary>
        public Vector3 OceanTileSize;

        /// <summary>
        /// The material of simple ocean.
        /// </summary>
        public Material OceanMaterial;

        /// <summary>
        /// The enabled of reflection.
        /// </summary>
        public bool ReflectionEnabled;

        /// <summary>
        /// The surface color of ocean.
        /// </summary>
        [SerializeField]
        private Color mSurfaceColor = Color.white;

        /// <summary>
        /// Gets or sets the color of the surface.
        /// </summary>
        /// <value>The color of the surface.</value>
        public Color SurfaceColor
        {
            get { return mSurfaceColor; }
            set
            {
                if (mSurfaceColor != value)
                {
                    mSurfaceColor = value;
                    UpdateWaterColor();
                }
            }
        }

        /// <summary>
        /// The water color of ocean.
        /// </summary>
        [SerializeField]
        private Color mWaterColor = Color.blue;

        /// <summary>
        /// Gets or sets the color of the water.
        /// </summary>
        /// <value>The color of the water.</value>
        public Color WaterColor
        {
            get { return mWaterColor; }
            set
            {
                if (mWaterColor != value)
                {
                    mWaterColor = value;
                    UpdateWaterColor();
                }
            }
        }

        /// <summary>
        /// The normal map scale.
        /// </summary>
        private int mNormalMapScale = 8;

        /// <summary>
        /// Gets or sets the normal map scale.
        /// </summary>
        /// <value>The normal map scale.</value>
        public int NormalMapScale
        {
            get { return mNormalMapScale; }
            set
            {
                if (mNormalMapScale != value)
                {
                    mNormalMapScale = value;
                    InitializeWaveGenerator();
                }
            }
        }

        /// <summary>
        /// The x offset of wind.
        /// </summary>
        private float mWindX = 10.0f;

        /// <summary>
        /// Gets the x offset of wind.
        /// </summary>
        /// <value>The x offset of wind.</value>
        public float WindX
        {
            get { return mWindX; }
            set
            {
                if (mWindX != value)
                {
                    mWindX = value;
                    InitializeWaveGenerator();
                }
            }
        }

        /// <summary>
        /// The ocean simulation enabled.
        /// </summary>
        private bool mSimulationEnabled;

        /// <summary>
        /// The reciprocal of ocean tile size.
        /// </summary>
        private Vector2 mOceanTileSizeReciprocal;

        /// <summary>
        /// The reflection texture.
        /// </summary>
        private RenderTexture mReflectionTexture;

        /// <summary>
        /// The refraction texture.
        /// </summary>
        private RenderTexture mRefractionTexture;

        /// <summary>
        /// The height data of mesh surface.
        /// </summary>
        private ComplexF[] mWaterHeightData;

        /// <summary>
        /// The tangent of x.
        /// </summary>
        private ComplexF[] mTangentX;

        /// <summary>
        /// The vertex offset of wave spectra.
        /// </summary>
        private ComplexF[] mVertexSpectra;

        /// <summary>
        /// The normal map of wave spectra.
        /// </summary>
        private ComplexF[] mNormalMapSpectra;

        /// <summary>
        /// The geometry width.
        /// </summary>
        private int mGeometryWidth;

        /// <summary>
        /// The geometry height.
        /// </summary>
        private int mGeometryHeight;

        /// <summary>
        /// The base mesh.
        /// </summary>
        private Mesh mBaseMesh;

        /// <summary>
        /// The base value for vertices and uv coordinates.
        /// </summary>
        private Vector3[] mBaseHeights;

        /// <summary>
        /// The vertices of mesh.
        /// </summary>
        private Vector3[] mVertices;

        /// <summary>
        /// The normals of mesh.
        /// </summary>
        private Vector3[] mNormals;

        /// <summary>
        /// The tangents of mesh.
        /// </summary>
        private Vector4[] mTangents;

        /// <summary>
        /// The tiles LOD.
        /// </summary>
        private List<List<Mesh>> mTilesLOD;

        /// <summary>
        /// The offscreen camera for rendering reflection and refraction.
        /// </summary>
        private Camera mOffscreenCamera;

        /// <summary>
        /// The Transform object of offscreen camera.
        /// </summary>
        private Transform mOffscreenCameraTransform;

        /// <summary>
        /// The Transform object of main camera.
        /// </summary>
        private Transform mMainCameraTransform;

        /// <summary>
        /// The humidity parameter.
        /// </summary>
        private float mHumidity = 0.1f;

        /// <summary>
        /// The previous humidity value.
        /// </summary>
        private float mPrevHumidityValue = 0.1f;

        /// <summary>
        /// The next humidity value.
        /// </summary>
        private float mNextHumidityValue = 0.4f;

        /// <summary>
        /// The previous humidity update time.
        /// </summary>
        private float mPrevHumidityUpdateTime = 0.0f;

        /// <summary>
        /// The wave scale in real time.
        /// </summary>
        private float mWaveScaleRealTime;

        /// <summary>
        /// Use this for initialization.
        /// </summary>
        private void Start()
        {
            // Initialize oceanTileSizeReciprocal when script start.
            mOceanTileSizeReciprocal = VectorUtility.GetVector2Reciprocal(OceanTileSize);

            SetupOffscreenRendering();

            mWaterHeightData = new ComplexF[TilePolygonWidth * TilePolygonHeight];
            mTangentX = new ComplexF[TilePolygonWidth * TilePolygonHeight];
            mGeometryWidth = TilePolygonWidth + 1;
            mGeometryHeight = TilePolygonHeight + 1;

            // Initialize tiles LOD list.
            mTilesLOD = new List<List<Mesh>>();

            for (int i = 0, count = MAX_LOD; i < count; ++i)
            {
                mTilesLOD.Add(new List<Mesh>());
            }

            // Initialize ocean tiles.
            for (int y = 0; y < TilesCount; ++y)
            {
                for (int x = 0; x < TilesCount; ++x)
                {
                    GameObject tile = new GameObject("Ocean Tile");

                    tile.transform.position = new Vector3(
                        transform.position.x + (x - Mathf.Floor(TilesCount * 0.5f)) * OceanTileSize.x,
                        transform.position.y,
                        transform.position.z + (y - Mathf.Floor(TilesCount * 0.5f)) * OceanTileSize.z
                        );

                    MeshFilter meshFilter = tile.AddComponent<MeshFilter>();
                    Mesh mesh = meshFilter.mesh;

                    mesh.vertices = new Vector3[3] { Vector3.zero, Vector3.zero, Vector3.zero };
                    mesh.normals = mesh.vertices;

                    tile.AddComponent<MeshRenderer>();
                    tile.renderer.material = OceanMaterial;
                    tile.transform.parent = transform;

                    // Set layer to the parent layer.
                    tile.layer = gameObject.layer;
                    mTilesLOD[0].Add(meshFilter.mesh);
                }
            }

            // Initialize wave spectra. v0 for vertex offset, n0 for normal map.
            mVertexSpectra = new ComplexF[TilePolygonWidth * TilePolygonHeight];
            mNormalMapSpectra = new ComplexF[NormalMapWidth * NormalMapHeight];

            InitializeWaveGenerator();
            UpdateWaterColor();
            GenHeightmap();

            if (Camera.main != null)
                mMainCameraTransform = Camera.main.transform;

            if (VisibleSimulation)
                mSimulationEnabled = false;
            else
                mSimulationEnabled = true;

            //Update Wave
            StartCoroutine(UpdateWave());
        }

        /// <summary>
        /// Called when the behaviour becomes disabled () or inactive.
        /// </summary>
        private void OnDisable()
        {
            DestroyReflectionTexture();
            DestroyRefractionTexture();
        }

        /// <summary>
        /// Called when the renderer is no longer visible by any camera.
        /// </summary>
        private void OnBecameVisible()
        {
            if (VisibleSimulation)
                mSimulationEnabled = true;
        }

        /// <summary>
        /// Called when the renderer became visible by any camera.
        /// </summary>
        private void OnBecameInvisible()
        {
            if (VisibleSimulation)
                mSimulationEnabled = false;
        }

        /// <summary>
        /// Update is called once per frame.
        /// </summary>
        private void Update()
        {
            if (!mSimulationEnabled)
                return;

            // Calculate mesh vertices, uv and tangents.
            float halfWidth = TilePolygonWidth / 2.0f;
            float halfHeight = TilePolygonHeight / 2.0f;
            float time = Time.time;

            for (int y = 0; y < TilePolygonHeight; ++y)
            {
                for (int x = 0; x < TilePolygonWidth; ++x)
                {
                    int idx = TilePolygonWidth * y + x;
                    float yCopy = y < halfHeight ? y : y - TilePolygonHeight;
                    float xCopy = x < halfWidth ? x : x - TilePolygonWidth;
                    Vector2 vecK = new Vector2(2.0f * Mathf.PI * xCopy / OceanTileSize.x, 2.0f * Mathf.PI * yCopy / OceanTileSize.z);

                    float sqrtMagnitude = (float)Math.Sqrt(Mathf.Pow(vecK.x, 2.0f) + Mathf.Pow(vecK.y, 2.0f));
                    float offset = Mathf.Sqrt(GRAVITY_ACCELERATION * sqrtMagnitude) * time * WaveSpeed;
                    ComplexF complexFA = new ComplexF(Mathf.Cos(offset), Mathf.Sin(offset));
                    ComplexF complexFB;
                    complexFB.Re = complexFA.Re;
                    complexFB.Im = -complexFA.Im;

                    int nY = y > 0 ? TilePolygonHeight - y : 0;
                    int nX = x > 0 ? TilePolygonWidth - x : 0;

                    mWaterHeightData[idx] = mVertexSpectra[idx] * complexFA + mVertexSpectra[TilePolygonWidth * nY + nX].GetConjugate() * complexFB;
                    mTangentX[idx] = mWaterHeightData[idx] * new ComplexF(0.0f, vecK.x) - mWaterHeightData[idx] * vecK.y;

                    // Choppy wave calculation.
                    if (x + y > 0)
                        mWaterHeightData[idx] += mWaterHeightData[idx] * vecK.x / sqrtMagnitude;
                }
            }

            Fourier.FFT2(mWaterHeightData, TilePolygonWidth, TilePolygonHeight, FourierDirection.Backward);
            Fourier.FFT2(mTangentX, TilePolygonWidth, TilePolygonHeight, FourierDirection.Backward);

            // Get base values for vertices and uv coordinates.
            if (mBaseHeights == null)
            {
                mBaseHeights = mBaseMesh.vertices;
                mVertices = new Vector3[mBaseHeights.Length];
                mNormals = new Vector3[mBaseHeights.Length];
                mTangents = new Vector4[mBaseHeights.Length];
            }

            int area = TilePolygonWidth * TilePolygonHeight;
            float scaleX = ChoppyScale / area;
            float scaleY = mWaveScaleRealTime / area;
            float scaleYReciprocal = MathUtility.GetReciprocal(scaleY);

            for (int i = 0; i < area; ++i)
            {
                int index = i + i / TilePolygonWidth;
                mVertices[index] = mBaseHeights[index];
                mVertices[index].x += mWaterHeightData[i].Im * scaleX;
                mVertices[index].y = mWaterHeightData[i].Re * scaleY;

                mNormals[index] = Vector3.Normalize(new Vector3(mTangentX[i].Re, scaleYReciprocal, mTangentX[i].Im));

                if ((i + 1) % TilePolygonWidth == 0)
                {
                    int indexPlus = index + 1;
                    int iWidth = i + 1 - TilePolygonWidth;
                    mVertices[indexPlus] = mBaseHeights[indexPlus];
                    mVertices[indexPlus].x += mWaterHeightData[iWidth].Im * scaleX;
                    mVertices[indexPlus].y = mWaterHeightData[iWidth].Re * scaleY;

                    mNormals[indexPlus] = Vector3.Normalize(new Vector3(mTangentX[iWidth].Re, scaleYReciprocal, mTangentX[iWidth].Im));
                }
            }

            int indexOffset = mGeometryWidth * (mGeometryHeight - 1);

            for (int i = 0; i < mGeometryWidth; ++i)
            {
                int index = i + indexOffset;
                int mod = i % TilePolygonWidth;

                mVertices[index] = mBaseHeights[index];
                mVertices[index].x += mWaterHeightData[mod].Im * scaleX;
                mVertices[index].y = mWaterHeightData[mod].Re * scaleY;

                mNormals[index] = Vector3.Normalize(new Vector3(mTangentX[mod].Re, scaleYReciprocal, mTangentX[mod].Im));
            }

            int geometryArea = mGeometryWidth * mGeometryHeight - 1;

            for (int i = 0; i < geometryArea; ++i)
            {
                Vector3 tmp;

                if ((i + 1) % mGeometryWidth == 0)
                    tmp = Vector3.Normalize(mVertices[i - TilePolygonWidth + 1] + new Vector3(OceanTileSize.x, 0.0f, 0.0f) - mVertices[i]);
                else
                    tmp = Vector3.Normalize(mVertices[i + 1] - mVertices[i]);

                mTangents[i] = new Vector4(tmp.x, tmp.y, tmp.z, mTangents[i].w);
            }

            for (int y = 0; y < mGeometryHeight; ++y)
            {
                for (int x = 0; x < mGeometryWidth; ++x)
                {
                    int index = x + mGeometryWidth * y;

                    if (x + 1 >= mGeometryWidth)
                    {
                        mTangents[index].w = mTangents[mGeometryWidth * y].w;
                        continue;
                    }

                    if (y + 1 >= mGeometryHeight)
                    {
                        mTangents[index].w = mTangents[x].w;
                        continue;
                    }

                    float right = mVertices[x + 1 + mGeometryWidth * y].x - mVertices[index].x;
                    float foam = right / (OceanTileSize.x / mGeometryWidth);

                    if (foam < 0.0f)
                        mTangents[index].w = 1.0f;
                    else if (foam < 0.5f)
                        mTangents[index].w += 3.0f * Time.deltaTime;
                    else
                        mTangents[index].w -= 0.4f * Time.deltaTime;

                    mTangents[index].w = Mathf.Clamp(mTangents[index].w, 0.0f, 2.0f);
                }
            }

            mTangents[geometryArea] = Vector4.Normalize(mVertices[geometryArea] + new Vector3(OceanTileSize.x, 0.0f, 0.0f) - mVertices[1]);

            for (int level = 0; level < MAX_LOD; ++level)
            {
                int pow = (int)Math.Pow(2.0f, level);
                int length = (int)((TilePolygonHeight / pow + 1) * (TilePolygonWidth / pow + 1));

                Vector4[] tangentsLOD = new Vector4[length];
                Vector3[] verticesLOD = new Vector3[length];
                Vector3[] normalsLOD = new Vector3[length];

                int index = 0;

                for (int y = 0; y < mGeometryHeight; y += pow)
                {
                    for (int x = 0; x < mGeometryWidth; x += pow)
                    {
                        int indexTemp = mGeometryWidth * y + x;
                        verticesLOD[index] = mVertices[indexTemp];
                        tangentsLOD[index] = mTangents[indexTemp];
                        normalsLOD[index++] = mNormals[indexTemp];
                    }
                }

                for (int i = 0, count = mTilesLOD[level].Count; i < count; ++i)
                {
                    Mesh meshLOD = mTilesLOD[level][i];
                    meshLOD.vertices = verticesLOD;
                    meshLOD.normals = normalsLOD;
                    meshLOD.tangents = tangentsLOD;
                }
            }

            if (ReflectionEnabled)
                RenderReflectionAndRefraction();
        }

        /// <summary>
        /// Creates the render textures of reflection and refraction.
        /// </summary>
        private void CreateRenderTextures()
        {
            if (ReflectionEnabled)
            {
                mReflectionTexture = new RenderTexture(RenderTextureWidth, RenderTextureHeight, 0);
                mRefractionTexture = new RenderTexture(RenderTextureWidth, RenderTextureHeight, 0);

                mReflectionTexture.wrapMode = TextureWrapMode.Clamp;
                mRefractionTexture.wrapMode = TextureWrapMode.Clamp;

                mReflectionTexture.isPowerOfTwo = true;
                mRefractionTexture.isPowerOfTwo = true;

                OceanMaterial.SetTexture("_Reflection", mReflectionTexture);
                OceanMaterial.SetTexture("_Refraction", mRefractionTexture);
                OceanMaterial.SetVector("_MaterialSize", new Vector4(OceanTileSize.x, OceanTileSize.y, OceanTileSize.z, 0.0f));
            }
        }

        /// <summary>
        /// Setup settings of offscreen rendering.
        /// </summary>
        private void SetupOffscreenRendering()
        {
            if (OceanMaterial != null)
                OceanMaterial.SetVector("_LightDir", mLightDirection);

            // if renderer reflection and refraction textures.
            CreateRenderTextures();

            // Create offscreeen camera to render reflection and refraction.
            GameObject cameraObj = new GameObject();
            cameraObj.name = "Offscreeen Camera";
            cameraObj.transform.parent = transform;

            mOffscreenCamera = cameraObj.AddComponent<Camera>();
            mOffscreenCamera.clearFlags = CameraClearFlags.Color;
            mOffscreenCamera.depth = -1;
            mOffscreenCamera.enabled = false;
            mOffscreenCameraTransform = mOffscreenCamera.transform;

            // Add MeshRenderer component.
            gameObject.AddComponent<MeshRenderer>();

            // Setup renderer.
            renderer.material.renderQueue = 1001;
            renderer.receiveShadows = false;
            renderer.castShadows = false;

            // Setup ocean surface mesh.
            Mesh mesh = new Mesh();

            Vector3[] vertices = new Vector3[4];
            Vector2[] uv = new Vector2[4];
            int[] triangles = new int[6];

            float minSizeX = -1024.0f;
            float maxSizeX = 1024.0f;

            float minSizeY = -1024.0f;
            float maxSizeY = 1024.0f;

            //The coordinates of vertices.
            vertices[0] = new Vector3(minSizeX, 0.0f, maxSizeY);
            vertices[1] = new Vector3(maxSizeX, 0.0f, maxSizeY);
            vertices[2] = new Vector3(maxSizeX, 0.0f, minSizeY);
            vertices[3] = new Vector3(minSizeX, 0.0f, minSizeY);

            triangles[0] = 0;
            triangles[1] = 1;
            triangles[2] = 2;
            triangles[3] = 2;
            triangles[4] = 1;
            triangles[5] = 0;

            mesh.vertices = vertices;
            mesh.uv = uv;
            mesh.normals = new Vector3[4];
            mesh.triangles = triangles;

            MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();

            if (meshFilter == null)
                meshFilter = gameObject.AddComponent<MeshFilter>();

            meshFilter.mesh = mesh;
            mesh.RecalculateBounds();

            // Prevent bounds will be calculated automatically.
            vertices[0] = Vector3.zero;
            vertices[1] = Vector3.zero;
            vertices[2] = Vector3.zero;
            vertices[3] = Vector3.zero;

            mesh.vertices = vertices;
        }

        /// <summary>
        /// Renders the reflection and refraction of ocean.
        /// </summary>
        private void RenderReflectionAndRefraction()
        {
            if (Camera.current == mOffscreenCamera)
                return;

            if (mReflectionTexture == null || mRefractionTexture == null)
                return;

            if (mMainCameraTransform == null)
                mMainCameraTransform = Camera.main.transform;

            Camera mainCamera = Camera.main;
            Matrix4x4 originalWorldToCamera = mainCamera.worldToCameraMatrix;
            int cullingMask = ~(1 << 4) & RenderLayers.value;

            // Reflection pass.
            float y = -transform.position.y;
            Matrix4x4 reflectionMatrix = Matrix4x4.zero;
            CameraUtility.CalculateReflectionMatrix(ref reflectionMatrix, new Vector4(0.0f, 1.0f, 0.0f, y));

            mOffscreenCamera.backgroundColor = RenderSettings.fogColor;
            mOffscreenCameraTransform.position = reflectionMatrix.MultiplyPoint(mMainCameraTransform.position);
            mOffscreenCameraTransform.rotation = mMainCameraTransform.rotation;
            mOffscreenCamera.worldToCameraMatrix = originalWorldToCamera * reflectionMatrix;
            mOffscreenCamera.cullingMask = cullingMask;
            mOffscreenCamera.targetTexture = mReflectionTexture;

            // Need to reverse face culling for reflection pass, since the camera is now flipped upside/downside.
            GL.SetRevertBackfacing(true);

            Vector4 cameraSpaceClipPlane = CameraUtility.CameraSpaceClipPlane(mOffscreenCamera, new Vector3(0.0f, transform.position.y, 0.0f), Vector3.up, 1.0f);
            Matrix4x4 projection = mainCamera.projectionMatrix;
            Matrix4x4 obliqueProjection = projection;

            mOffscreenCamera.fieldOfView = mainCamera.fieldOfView;
            mOffscreenCamera.aspect = mainCamera.aspect;

            CameraUtility.CalculateObliqueMatrix(ref obliqueProjection, cameraSpaceClipPlane);

            // Do the actual render, with the near plane set as the clipping plane. See the pro water source for details.
            mOffscreenCamera.projectionMatrix = obliqueProjection;

            if (!ReflectionEnabled)
                mOffscreenCamera.cullingMask = 0;

            mOffscreenCamera.Render();

            GL.SetRevertBackfacing(false);

            // Refraction pass.
            mOffscreenCamera.cullingMask = cullingMask;
            mOffscreenCamera.targetTexture = mRefractionTexture;
            obliqueProjection = projection;
            mOffscreenCameraTransform.position = mMainCameraTransform.position;
            mOffscreenCameraTransform.rotation = mMainCameraTransform.rotation;
            mOffscreenCamera.worldToCameraMatrix = originalWorldToCamera;

            cameraSpaceClipPlane = CameraUtility.CameraSpaceClipPlane(mOffscreenCamera, Vector3.zero, Vector3.up, -1.0f);
            CameraUtility.CalculateObliqueMatrix(ref obliqueProjection, cameraSpaceClipPlane);
            mOffscreenCamera.projectionMatrix = obliqueProjection;
            mOffscreenCamera.Render();
            mOffscreenCamera.projectionMatrix = projection;
            mOffscreenCamera.targetTexture = null;
        }

        /// <summary>
        /// Initializes the wave generator.
        /// </summary>
        private void InitializeWaveGenerator()
        {
            // Wind was restricted to one direction, reduces calculations.
            Vector2 wind = new Vector2(mWindX, 0.0f);

            for (int y = 0; y < TilePolygonHeight; ++y)
            {
                for (int x = 0; x < TilePolygonWidth; ++x)
                {
                    float yCopy = y < TilePolygonHeight / 2.0f ? y : y - TilePolygonHeight;
                    float xCopy = x < TilePolygonWidth / 2.0f ? x : x - TilePolygonWidth;
                    Vector2 vecK = new Vector2(2.0f * Mathf.PI * xCopy / OceanTileSize.x, 2.0f * Mathf.PI * yCopy / OceanTileSize.z);
                    mVertexSpectra[TilePolygonWidth * y + x] = new ComplexF(MathUtility.GenGaussianRnd(), MathUtility.GenGaussianRnd()) * 0.707f * (float)Math.Sqrt(PhillipsSpectrum(vecK, wind));
                }
            }

            for (int y = 0; y < RenderTextureHeight; ++y)
            {
                for (int x = 0; x < RenderTextureWidth; ++x)
                {
                    float yCopy = y < RenderTextureHeight / 2.0f ? y : y - RenderTextureHeight;
                    float xCopy = x < RenderTextureWidth / 2.0f ? x : x - RenderTextureWidth;
                    Vector2 vecK = new Vector2(2.0f * Mathf.PI * xCopy / (OceanTileSize.x / mNormalMapScale), 2.0f * Mathf.PI * yCopy / (OceanTileSize.z / mNormalMapScale));
                    mNormalMapSpectra[RenderTextureWidth * y + x] = new ComplexF(MathUtility.GenGaussianRnd(), MathUtility.GenGaussianRnd() * 0.707f * (float)Math.Sqrt(PhillipsSpectrum(vecK, wind)));
                }
            }
        }

        /// <summary>
        /// Updates the wave.
        /// </summary>
        /// <returns>IEnumerator.</returns>
        private IEnumerator UpdateWave()
        {
            while (true)
            {
                yield return new WaitForEndOfFrame();

                if (ForceStorm)
                    mHumidity = FORCE_STORM_HUMIDITY;
                else
                    mHumidity = GetHumidity();

                mWaveScaleRealTime = Mathf.Lerp(0.0f, WaveScale, mHumidity);
            }
        }

        /// <summary>
        /// Updates the color of the water.
        /// </summary>
        private void UpdateWaterColor()
        {
            OceanMaterial.SetColor("_SurfaceColor", mSurfaceColor);
            OceanMaterial.SetColor("_WaterColor", mWaterColor);
        }

        /// <summary>
        /// Updates the light direction.
        /// </summary>
        private void UpdateLightDirection()
        {
            OceanMaterial.SetVector("_LightDir", mLightDirection);
        }

        /// <summary>
        /// Generate the heightmap.
        /// </summary>
        private void GenHeightmap()
        {
            Mesh mesh = new Mesh();

            int y = 0;
            int x = 0;

            // Build vertices and uv.
            Vector3[] vertices = new Vector3[mGeometryWidth * mGeometryHeight];
            Vector4[] tangents = new Vector4[mGeometryWidth * mGeometryHeight];
            Vector2[] uv = new Vector2[mGeometryWidth * mGeometryHeight];

            Vector2 uvScale = new Vector2(1.0f / (mGeometryWidth - 1.0f), 1.0f / (mGeometryHeight - 1.0f));
            Vector3 sizeScale = new Vector3(OceanTileSize.x / (mGeometryWidth - 1.0f), OceanTileSize.y, OceanTileSize.z / (mGeometryHeight - 1.0f));

            for (y = 0; y < mGeometryHeight; ++y)
            {
                for (x = 0; x < mGeometryWidth; ++x)
                {
                    Vector3 vertex = new Vector3(x, 0.0f, y);
                    vertices[y * mGeometryWidth + x] = Vector3.Scale(vertex, sizeScale);
                    uv[y * mGeometryWidth + x] = Vector2.Scale(new Vector2(x, y), uvScale);
                }
            }

            mesh.vertices = vertices;
            mesh.uv = uv;

            // Build tangents.
            for (y = 0; y < mGeometryHeight; ++y)
            {
                for (x = 0; x < mGeometryWidth; ++x)
                {
                    tangents[y * mGeometryWidth + x] = new Vector4(1.0f, 0.0f, 0.0f, -1.0f);
                }
            }

            mesh.tangents = tangents;

            int i = 0;
            int j = 0;
            int count = MAX_LOD;

            for (i = 0; i < count; ++i)
            {
                int idx = 0;
                double pow = Math.Pow(2, i);

                int length = (int)(TilePolygonHeight / pow + 1) * (int)(TilePolygonWidth / pow + 1);
                Vector3[] verticesLOD = new Vector3[length];
                Vector2[] uvLOD = new Vector2[length];

                for (y = 0; y < mGeometryHeight; y += (int)pow)
                {
                    for (x = 0; x < mGeometryWidth; x += (int)pow)
                    {
                        verticesLOD[idx] = vertices[mGeometryWidth * y + x];
                        uvLOD[idx++] = uv[mGeometryWidth * y + x];
                    }
                }

                int tilesLODCount = mTilesLOD[i].Count;

                for (j = 0; j < tilesLODCount; ++j)
                {
                    Mesh meshLOD = mTilesLOD[i][j];
                    meshLOD.vertices = verticesLOD;
                    meshLOD.uv = uvLOD;
                    meshLOD.tangents = tangents;
                }
            }

            // Build triangle indices: 3 indices into vertex array for each triangle.
            for (i = 0; i < count; ++i)
            {
                int idx = 0;
                double pow = Math.Pow(2, i);
                int widthLOD = (int)(TilePolygonWidth / pow + 1);
                int[] triangles = new int[(int)(TilePolygonHeight / pow * TilePolygonWidth / pow) * 6];

                int height = (int)(TilePolygonHeight / pow);
                int width = (int)(TilePolygonWidth / pow);

                for (y = 0; y < height; ++y)
                {
                    for (x = 0; x < width; ++x)
                    {
                        // For each grid cell output two triangle.
                        triangles[idx++] = y * widthLOD + x;
                        triangles[idx++] = (y + 1) * widthLOD + x;
                        triangles[idx++] = y * widthLOD + x + 1;

                        triangles[idx++] = (y + 1) * widthLOD + x;
                        triangles[idx++] = (y + 1) * widthLOD + x + 1;
                        triangles[idx++] = y * widthLOD + x + 1;
                    }
                }

                int tilesLODCount = mTilesLOD[i].Count;

                for (j = 0; j < tilesLODCount; ++j)
                {
                    Mesh meshLOD = mTilesLOD[i][j];
                    meshLOD.triangles = triangles;
                }
            }

            mBaseMesh = mesh;
        }

        /// <summary>
        /// Phillips Spectrum algorithm.
        /// </summary>
        /// <param name="vecK">The vector K.</param>
        /// <param name="wind">The vector wind.</param>
        /// <returns>System.Single.</returns>
        private float PhillipsSpectrum(Vector2 vecK, Vector2 wind)
        {
            float A = vecK.x > 0.0f ? 1.0f : 0.05f; // Set wind to blow only in one direction - otherwise we get turmoiling water.

            float L = wind.sqrMagnitude / 9.81f;
            float k2 = vecK.sqrMagnitude;

            // Avoid division by zero
            if (k2 == 0.0f)
                return k2;

            float vecKMagnitude = vecK.magnitude;
            return (float)(A * Math.Exp(-1.0f / (k2 * L * L) - Math.Pow(vecKMagnitude * 0.1, 2.0f)) / (k2 * k2) * Math.Pow(Vector2.Dot(vecK / vecKMagnitude, wind / wind.magnitude), 2.0f));
        }

        /// <summary>
        /// This function return smooth random value from 0 to 1, used for smooth waves scale calculation ®"MindBlocks".
        /// </summary>
        /// <returns>System.Single.</returns>
        private float GetHumidity()
        {
            float time = Time.time;

            int intTime = (int)(time * HUMIDITY_UPDATE_FREQUENCY);
            int intPrevTime = (int)(mPrevHumidityUpdateTime * HUMIDITY_UPDATE_FREQUENCY);

            if (intTime != intPrevTime)
            {
                mPrevHumidityValue = mNextHumidityValue;
                mNextHumidityValue = UnityEngine.Random.value;
            }

            mPrevHumidityValue = time;
            float t = time * HUMIDITY_UPDATE_FREQUENCY - intTime;

            return Mathf.SmoothStep(mPrevHumidityValue, mNextHumidityValue, t);
        }

        /// <summary>
        /// Destroys the reflection texture.
        /// </summary>
        private void DestroyReflectionTexture()
        {
            if (mReflectionTexture != null)
                DestroyImmediate(mReflectionTexture);

            mReflectionTexture = null;
        }

        /// <summary>
        /// Destroys the refraction texture.
        /// </summary>
        private void DestroyRefractionTexture()
        {
            if (mRefractionTexture != null)
                DestroyImmediate(mRefractionTexture);

            mRefractionTexture = null;
        }
    }
}