import EasyRoads3D;
@CustomEditor(MarkerScript)
@CanEditMultipleObjects
class MarkerEditorScript extends Editor
{
var oldPos : Vector3;
var pos : Vector3;
var ODCQCOODCC : GUISkin;
var OQQOQODQDO : GUISkin;
var showGui : int;
var OQCDDDOODD : boolean;
var count:int = 0;
function OnEnable(){
if(target.objectScript == null) target.SetObjectScript();
else target.GetMarkerCount();
}
function OnInspectorGUI()
{


if(target.objectScript.OCCCCCQQQDs == null)target.objectScript.OCCCCCQQQDs = new GameObject[0];
showGui = EasyRoadsGUIMenu(false, false, target.objectScript);
if(showGui != -1 && !target.objectScript.ODODDQOO) Selection.activeGameObject =  target.transform.parent.parent.gameObject;
else if(target.objectScript.OCCCCCQQQDs.length <= 1  && !target.objectScript.ODODDDOO) ERMarkerGUI(target);
else  if(target.objectScript.OCCCCCQQQDs.length == 2 && !target.objectScript.ODODDDOO) MSMarkerGUI(target);
else if(target.objectScript.ODODDDOO)TRMarkerGUI(target);


}
function OnSceneGUI() {
if(target.objectScript.ODQQOCODOC == null || target.objectScript.erInit == "") Selection.activeGameObject =  target.transform.parent.parent.gameObject;
else MarkerOnScene(target);
}
function EasyRoadsGUIMenu(flag : boolean, senderIsMain : boolean,  nRoadScript : RoadObjectScript) : int {
if((target.objectScript.OOCCCDOOCQ == null || target.objectScript.OCCCDQDOQC == null || target.objectScript.OQCQDDCCCC == null) && target.objectScript.ODQQOCODOC){
target.objectScript.OOCCCDOOCQ = new boolean[5];
target.objectScript.OCCCDQDOQC = new boolean[5];
target.objectScript.OQCQDDCCCC = nRoadScript;

target.objectScript.OODDCOQCOO = target.objectScript.ODQQOCODOC.OQDDQQODCO();
target.objectScript.ODODQOQO = target.objectScript.ODQQOCODOC.ODQOQCDCQC();
target.objectScript.ODODQOQOInt = target.objectScript.ODQQOCODOC.OQOQODOQQO();
}else if(target.objectScript.ODQQOCODOC == null) return;

if(target.objectScript.ODCQCOODCC == null){
target.objectScript.ODCQCOODCC = Resources.Load("ER3DSkin", GUISkin);
target.objectScript.OQQOQQODDQ = Resources.Load("ER3DLogo", Texture2D);
}
if(!flag) target.objectScript.OQCOOOQOOD();
GUI.skin = target.objectScript.ODCQCOODCC;
EditorGUILayout.Space();
EditorGUILayout.BeginHorizontal ();
GUILayout.FlexibleSpace();
target.objectScript.OOCCCDOOCQ[0] = GUILayout.Toggle(target.objectScript.OOCCCDOOCQ[0] ,new GUIContent("", " Add road markers. "),"AddMarkers",GUILayout.Width(40), GUILayout.Height(22));
if(target.objectScript.OOCCCDOOCQ[0] == true && target.objectScript.OCCCDQDOQC[0] == false) {
target.objectScript.OQCOOOQOOD();
target.objectScript.OOCCCDOOCQ[0] = true;  target.objectScript.OCCCDQDOQC[0] = true;
Selection.activeGameObject =  target.transform.parent.parent.gameObject;
}
target.objectScript.OOCCCDOOCQ[1]  = GUILayout.Toggle(target.objectScript.OOCCCDOOCQ[1] ,new GUIContent("", " Insert road markers. "),"insertMarkers",GUILayout.Width(40),GUILayout.Height(22));
if(target.objectScript.OOCCCDOOCQ[1] == true && target.objectScript.OCCCDQDOQC[1] == false) {
target.objectScript.OQCOOOQOOD();
target.objectScript.OOCCCDOOCQ[1] = true;  target.objectScript.OCCCDQDOQC[1] = true;
Selection.activeGameObject =  target.transform.parent.parent.gameObject;
}
target.objectScript.OOCCCDOOCQ[2]  = GUILayout.Toggle(target.objectScript.OOCCCDOOCQ[2] ,new GUIContent("", " Process the terrain and create road geometry. "),"terrain",GUILayout.Width(40),GUILayout.Height(22));

if(target.objectScript.OOCCCDOOCQ[2] == true && target.objectScript.OCCCDQDOQC[2] == false) {
if(target.objectScript.markers < 2){
EditorUtility.DisplayDialog("Alert", "A minimum of 2 road markers is required before the terrain can be leveled!", "Close");
target.objectScript.OOCCCDOOCQ[2] = false;
}else{
target.objectScript.OOCCCDOOCQ[2] = false;
Selection.activeGameObject =  target.transform.parent.parent.gameObject;





}
}
if(target.objectScript.OOCCCDOOCQ[2] == false && target.objectScript.OCCCDQDOQC[2] == true){

target.objectScript.OCCCDQDOQC[2] = false;
Selection.activeGameObject =  target.transform.parent.parent.gameObject;
}
target.objectScript.OOCCCDOOCQ[3]  = GUILayout.Toggle(target.objectScript.OOCCCDOOCQ[3] ,new GUIContent("", " General settings. "),"settings",GUILayout.Width(40),GUILayout.Height(22));
if(target.objectScript.OOCCCDOOCQ[3] == true && target.objectScript.OCCCDQDOQC[3] == false) {
target.objectScript.OQCOOOQOOD();
target.objectScript.OOCCCDOOCQ[3] = true;  target.objectScript.OCCCDQDOQC[3] = true;
Selection.activeGameObject =  target.transform.parent.parent.gameObject;
}
target.objectScript.OOCCCDOOCQ[4]  = GUILayout.Toggle(target.objectScript.OOCCCDOOCQ[4] ,new GUIContent("", "Version and Purchase Info"),"info",GUILayout.Width(40),GUILayout.Height(22));
if(target.objectScript.OOCCCDOOCQ[4] == true && target.objectScript.OCCCDQDOQC[4] == false) {
target.objectScript.OQCOOOQOOD();
target.objectScript.OOCCCDOOCQ[4] = true;  target.objectScript.OCCCDQDOQC[4] = true;
Selection.activeGameObject =  target.transform.parent.parent.gameObject;
}
GUILayout.FlexibleSpace();
EditorGUILayout.EndHorizontal();
GUI.skin = null;
target.objectScript.OQOCDQDDOQ = -1;
for(var i : int  = 0; i < 5; i++){
if(target.objectScript.OOCCCDOOCQ[i]){
target.objectScript.OQOCDQDDOQ = i;
target.objectScript.ODCDCODQOC = i;
}
}
if(target.objectScript.OQOCDQDDOQ == -1) target.objectScript.OQCOOOQOOD();
var markerMenuDisplay : int = 1;
if(target.objectScript.OQOCDQDDOQ == 0 || target.objectScript.OQOCDQDDOQ == 1) markerMenuDisplay = 0;
else if(target.objectScript.OQOCDQDDOQ == 2 || target.objectScript.OQOCDQDDOQ == 3 || target.objectScript.OQOCDQDDOQ == 4) markerMenuDisplay = 0;
if(target.objectScript.OCOOQDQCQC && !target.objectScript.OOCCCDOOCQ[2] && !target.objectScript.ODODDQOO){
target.objectScript.OCOOQDQCQC = false;
if(target.objectScript.objectType != 2)target.objectScript.OCQOCDCCOO();
if(target.objectScript.objectType == 2 && target.objectScript.OCOOQDQCQC){
Debug.Log("restore");
target.objectScript.ODQQOCODOC.OQOOOCQCCC(target.transform, true);
}
}
GUI.skin.box.alignment = TextAnchor.UpperLeft;
if(target.objectScript.OQOCDQDDOQ >= 0 && target.objectScript.OQOCDQDDOQ != 4){
if(target.objectScript.OODDCOQCOO == null || target.objectScript.OODDCOQCOO.Length == 0){

target.objectScript.OODDCOQCOO = target.objectScript.ODQQOCODOC.OQDDQQODCO();
target.objectScript.ODODQOQO = target.objectScript.ODQQOCODOC.ODQOQCDCQC();
target.objectScript.ODODQOQOInt = target.objectScript.ODQQOCODOC.OQOQODOQQO();
}
EditorGUILayout.BeginHorizontal();
GUILayout.Box(target.objectScript.OODDCOQCOO[target.objectScript.OQOCDQDDOQ], GUILayout.MinWidth(253), GUILayout.MaxWidth(1500), GUILayout.Height(50));
EditorGUILayout.EndHorizontal();
EditorGUILayout.Space();
}
return target.objectScript.OQOCDQDDOQ;
}
function ERMarkerGUI( markerScript : MarkerScript){
EditorGUILayout.Space();
EditorGUILayout.BeginVertical("Box");
EditorGUILayout.LabelField(" Marker: " + (target.markerNum + 1).ToString(), EditorStyles.boldLabel);
EditorGUILayout.EndVertical();
EditorGUILayout.Space();
if(target.distance == "-1" && target.objectScript.ODQQOCODOC != null){
var arr = target.objectScript.ODQQOCODOC.OCCDQCOCDD(target.markerNum);
target.distance = arr[0];
target.OQQCQOQCQC = arr[1];
target.OCDODQCCCC = arr[2];
}
GUILayout.Label("   Total Distance to Marker: " + target.distance.ToString() + " km");
GUILayout.Label("   Segment Distance to Marker: " + target.OQQCQOQCQC.ToString() + " km");
GUILayout.Label("   Marker Distance: " + target.OCDODQCCCC.ToString() + " m");
EditorGUILayout.Space();
EditorGUILayout.BeginVertical("Box");
EditorGUILayout.LabelField(" Marker Settings", EditorStyles.boldLabel);
EditorGUILayout.EndVertical();
var oldss : boolean = markerScript.OQCQDDCQDQ;
EditorGUILayout.BeginHorizontal();
GUILayout.Label(new GUIContent("    Soft Selection", "When selected, the settings of other road markers within the selected distance will change according their distance to this marker."),  GUILayout.Width(105));
GUI.SetNextControlName ("OQCQDDCQDQ");
markerScript.OQCQDDCQDQ = EditorGUILayout.Toggle (markerScript.OQCQDDCQDQ);
EditorGUILayout.EndHorizontal();
if(markerScript.OQCQDDCQDQ){
EditorGUILayout.BeginHorizontal();
GUILayout.Label(new GUIContent("         Distance", "The soft selection distance within other markers should change too."),  GUILayout.Width(105));
markerScript.OQDQOQQQOC = EditorGUILayout.Slider(markerScript.OQDQOQQQOC, 0, 500);
EditorGUILayout.EndHorizontal();
EditorGUILayout.Space();
}
if(oldss != markerScript.OQDQOQQQOC) target.objectScript.ResetMaterials(markerScript);

EditorGUILayout.BeginHorizontal();
GUILayout.Label(new GUIContent("    Left Indent", "The distance from the left side of the road to the part of the terrain levelled at the same height as the road") ,  GUILayout.Width(105));
GUI.SetNextControlName ("ri");
oldfl = markerScript.ri;
markerScript.ri = EditorGUILayout.Slider(markerScript.ri, target.objectScript.indent, 100);
EditorGUILayout.EndHorizontal();
if(oldfl != markerScript.ri){
target.objectScript.OQDQOCDCCO("ri", markerScript);
markerScript.OOQOQQOO = markerScript.ri;
}

EditorGUILayout.BeginHorizontal();
GUILayout.Label(new GUIContent("    Right Indent", "The distance from the right side of the road to the part of the terrain levelled at the same height as the road") ,  GUILayout.Width(105));
oldfl = markerScript.li;
markerScript.li =  EditorGUILayout.Slider(markerScript.li, target.objectScript.indent, 100);
EditorGUILayout.EndHorizontal();
if(oldfl != markerScript.li){
target.objectScript.OQDQOCDCCO("li", markerScript);
markerScript.ODODQQOO = markerScript.li;
}

EditorGUILayout.BeginHorizontal();
GUILayout.Label(new GUIContent("    Left Surrounding", "This represents the distance over which the terrain will be gradually leveled on the left side of the road to the original terrain height"),  GUILayout.Width(105));
oldfl = markerScript.rs;
GUI.SetNextControlName ("rs");
markerScript.rs = EditorGUILayout.Slider(markerScript.rs,  target.objectScript.indent, 100);
EditorGUILayout.EndHorizontal();
if(oldfl != markerScript.rs){
target.objectScript.OQDQOCDCCO("rs", markerScript);
markerScript.ODOQQOOO = markerScript.rs;
}

EditorGUILayout.BeginHorizontal();
GUILayout.Label(new GUIContent("    Right Surrounding", "This represents the distance over which the terrain will be gradually leveled on the right side of the road to the original terrain height"),  GUILayout.Width(105));
oldfl = markerScript.ls;
markerScript.ls = EditorGUILayout.Slider(markerScript.ls,  target.objectScript.indent, 100);
EditorGUILayout.EndHorizontal();
if(oldfl != markerScript.ls){
target.objectScript.OQDQOCDCCO("ls", markerScript);
markerScript.DODOQQOO = markerScript.ls;
}
if(target.objectScript.objectType == 0){

EditorGUILayout.BeginHorizontal();
oldfl = markerScript.rt;
GUILayout.Label(new GUIContent("    Left Tilting", "Use this setting to tilt the road on the left side (m)."),  GUILayout.Width(105));

markerScript.rt = EditorGUILayout.Slider(markerScript.rt, 0, target.objectScript.roadWidth * 0.5f);


EditorGUILayout.EndHorizontal();
if(oldfl != markerScript.rt){
target.objectScript.OQDQOCDCCO("rt", markerScript);
markerScript.ODDQODOO = markerScript.rt;
}
EditorGUILayout.BeginHorizontal();
GUILayout.Label(new GUIContent("    Right Tilting", "Use this setting to tilt the road on the right side (cm)."),  GUILayout.Width(105));
oldfl = markerScript.lt;
markerScript.lt = EditorGUILayout.Slider(markerScript.lt, 0, target.objectScript.roadWidth * 0.5f);
EditorGUILayout.EndHorizontal();
if(oldfl != markerScript.lt){
target.objectScript.OQDQOCDCCO("lt", markerScript);
markerScript.ODDOQOQQ = markerScript.lt;
}

if(target.markerInt < 2){
GUILayout.Label(new GUIContent("    Bridge Objects are disabled for the first two markers!", ""));
}else{

EditorGUILayout.BeginHorizontal();
GUILayout.Label(new GUIContent("    Bridge Object", "When selected this road segment will be treated as a bridge segment."),  GUILayout.Width(105));
GUI.SetNextControlName ("bridgeObject");
markerScript.bridgeObject = EditorGUILayout.Toggle (markerScript.bridgeObject);
EditorGUILayout.EndHorizontal();
EditorGUILayout.BeginHorizontal();
if(markerScript.bridgeObject){
GUILayout.Label(new GUIContent("      Distribute Heights", "When selected the terrain, the terrain will be gradually leveled between the height of this road segment and the current terrain height of the inner bridge segment."),  GUILayout.Width(135));
GUI.SetNextControlName ("distHeights");
markerScript.distHeights = EditorGUILayout.Toggle (markerScript.distHeights);
}
EditorGUILayout.EndHorizontal();
}
EditorGUILayout.BeginHorizontal();
GUILayout.Label(new GUIContent("    Tunnel Object", "When selected this road segment will be treated as a tunnel segment."),  GUILayout.Width(105));
GUI.SetNextControlName ("bridgeObject");
markerScript.tunnelFlag = EditorGUILayout.Toggle (markerScript.tunnelFlag);
EditorGUILayout.EndHorizontal();
GUI.enabled = true;
}else{
EditorGUILayout.BeginHorizontal();
GUILayout.Label(new GUIContent("    Floor Depth", "Use this setting to change the floor depth for this marker."),  GUILayout.Width(105));
oldfl = markerScript.floorDepth;
markerScript.floorDepth = EditorGUILayout.Slider(markerScript.floorDepth, 0, 50);
EditorGUILayout.EndHorizontal();
if(oldfl != markerScript.floorDepth){
target.objectScript.OQDQOCDCCO("floorDepth", markerScript);
markerScript.oldFloorDepth = markerScript.floorDepth;
}
}
EditorGUILayout.Space();

if(target.objectScript.objectType == 0){
EditorGUILayout.BeginHorizontal();
GUILayout.Label(new GUIContent("    Start New LOD Segment", "Use this to split the road or river object in segments to use in LOD system."),  GUILayout.Width(170));
markerScript.newSegment = EditorGUILayout.Toggle (markerScript.newSegment);
EditorGUILayout.EndHorizontal();
}

EditorGUILayout.Space();
if(!markerScript.autoUpdate){
EditorGUILayout.BeginHorizontal();
GUILayout.FlexibleSpace();
if(GUILayout.Button ("Refresh Surface", GUILayout.Width(225))){
target.objectScript.OODQDODOCC();
}
GUILayout.FlexibleSpace();
EditorGUILayout.EndHorizontal();
}
if (GUI.changed && !target.objectScript.OODODOOOQC){
target.objectScript.OODODOOOQC = true;
}else if(target.objectScript.OODODOOOQC){
target.objectScript.OOQCQDQCOC(markerScript);
target.objectScript.OODODOOOQC = false;
SceneView.lastActiveSceneView.Repaint();
}
oldfl = markerScript.rs;
}
function MSMarkerGUI( markerScript : MarkerScript){
EditorGUILayout.Space();
EditorGUILayout.BeginHorizontal();
GUILayout.FlexibleSpace();
if(GUILayout.Button (new GUIContent(" Align XYZ", "Click to distribute the x, y and z values of all markers in between the selected markers in a line between the selected markers."), GUILayout.Width(150))){
Undo.RegisterUndo(target.transform.parent.GetComponentsInChildren(typeof(Transform)), "Marker align");
target.objectScript.ODQQOCODOC.OOOCDCCDCQ(target.objectScript.OCCCCCQQQDs, 0);
target.objectScript.OODQDODOCC();
}
GUILayout.FlexibleSpace();
EditorGUILayout.EndHorizontal();
EditorGUILayout.BeginHorizontal();
GUILayout.FlexibleSpace();
if(GUILayout.Button (new GUIContent(" Align XZ", "Click to distribute the x and z values of all markers in between the selected markers in a line between the selected markers."), GUILayout.Width(150))){
Undo.RegisterUndo(target.transform.parent.GetComponentsInChildren(typeof(Transform)), "Marker align");
target.objectScript.ODQQOCODOC.OOOCDCCDCQ(target.objectScript.OCCCCCQQQDs, 1);
target.objectScript.OODQDODOCC();
}
GUILayout.FlexibleSpace();
EditorGUILayout.EndHorizontal();
EditorGUILayout.BeginHorizontal();
GUILayout.FlexibleSpace();
if(GUILayout.Button (new GUIContent(" Align XZ  Snap Y", "Click to distribute the x and z values of all markers in between the selected markers in a line between the selected markers and snap the y value to the terrain height at the new position."), GUILayout.Width(150))){
Undo.RegisterUndo(target.transform.parent.GetComponentsInChildren(typeof(Transform)), "Marker align");
target.objectScript.ODQQOCODOC.OOOCDCCDCQ(target.objectScript.OCCCCCQQQDs, 2);
target.objectScript.OODQDODOCC();
}
GUILayout.FlexibleSpace();
EditorGUILayout.EndHorizontal();
EditorGUILayout.BeginHorizontal();
GUILayout.FlexibleSpace();
if(GUILayout.Button (new GUIContent(" Average Heights ", "Click to distribute the heights all markers in between the selected markers."), GUILayout.Width(150))){
Undo.RegisterUndo(target.transform.parent.GetComponentsInChildren(typeof(Transform)), "Marker align");
target.objectScript.ODQQOCODOC.OOOCDCCDCQ(target.objectScript.OCCCCCQQQDs, 3);
target.objectScript.OODQDODOCC();
}
GUILayout.FlexibleSpace();
EditorGUILayout.EndHorizontal();
EditorGUILayout.Space();
EditorGUILayout.Space();
}
function TRMarkerGUI(markerScript : MarkerScript){
EditorGUILayout.Space();

if(RoadObjectScript.ODODOQQO == null){
var arr = ODCDDQQDDC.OCDOQDQCOQ(false);
RoadObjectScript.ODODOQQO = ODCDDQQDDC.OCQCOCOODC(arr);
}
if(markerScript.ODDGDOOO.length == 0 ){
markerScript.ODDGDOOO = new boolean[RoadObjectScript.ODODOQQO.Length];
markerScript.ODDQOODO = new float[RoadObjectScript.ODODOQQO.Length];
markerScript.ODOQODOO = new float[RoadObjectScript.ODODOQQO.Length];
markerScript.ODDOQDO = new float[RoadObjectScript.ODODOQQO.Length];
for(i=0;i < RoadObjectScript.ODODOQQO.Length;i++){
markerScript.ODDGDOOO[i] = true;
markerScript.ODDQOODO[i] = 0;
markerScript.ODOQODOO[i] = 0;
markerScript.ODDOQDO[i] = 0;
}
}
GUI.skin = target.objectScript.OQQOQODQDO;
if(target.objectScript.ODODDQOO.Length > 0){
if(target.objectScript.ODOQOOQO >= target.objectScript.ODODDQOO.Length) target.objectScript.ODOQOOQO = 0;

EditorGUILayout.BeginHorizontal();
GUILayout.Label(new GUIContent(" Selected Object", ""), GUILayout.Width(95) );
target.objectScript.ODOQOOQO = EditorGUILayout.Popup (target.objectScript.ODOQOOQO, target.objectScript.ODODDQOO,   GUILayout.Width(125));
EditorGUILayout.EndHorizontal();
EditorGUILayout.Space();

EditorGUILayout.BeginHorizontal();
GUILayout.Label(new GUIContent(" Display Object", "This will activate/deactivate the side object for this marker"), GUILayout.Width(95) );
var ia : boolean = target.ODDGDOOO[target.objectScript.OOQQQOQO[target.objectScript.ODOQOOQO]];
target.ODDGDOOO[target.objectScript.OOQQQOQO[target.objectScript.ODOQOOQO]] = EditorGUILayout.Toggle (target.ODDGDOOO[target.objectScript.OOQQQOQO[target.objectScript.ODOQOOQO]]);
EditorGUILayout.EndHorizontal();
if(ia != target.ODDGDOOO[target.objectScript.OOQQQOQO[target.objectScript.ODOQOOQO]]){
ODCDDQQDDC.OCCQOCDQQO(target.objectScript.transform, target.objectScript.ODODDQOO[target.objectScript.ODOQOOQO]);
RoadObjectEditorScript.OOCQCCDOQQ(target.objectScript, target.objectScript.OOQQQOQO[target.objectScript.ODOQOOQO], target.objectScript);

}
EditorGUILayout.BeginHorizontal();
GUILayout.Label(new GUIContent(" Spline Node", "When checked this marker will be included as a control point in the spline shape"), GUILayout.Width(95) );
ia = target.ODDQOOO[target.objectScript.OOQQQOQO[target.objectScript.ODOQOOQO]];
target.ODDQOOO[target.objectScript.OOQQQOQO[target.objectScript.ODOQOOQO]] = EditorGUILayout.Toggle (target.ODDQOOO[target.objectScript.OOQQQOQO[target.objectScript.ODOQOOQO]]);
EditorGUILayout.EndHorizontal();
if(ia != target.ODDQOOO[target.objectScript.OOQQQOQO[target.objectScript.ODOQOOQO]]){
ODCDDQQDDC.OCCQOCDQQO(target.objectScript.transform, target.objectScript.ODODDQOO[target.objectScript.ODOQOOQO]);
RoadObjectEditorScript.OOCQCCDOQQ(target.objectScript, target.objectScript.OOQQQOQO[target.objectScript.ODOQOOQO], target.objectScript);

}
EditorGUILayout.BeginHorizontal();
GUILayout.Label(new GUIContent(" Distance Sideways", "This controls the sideways distance away from the road."), GUILayout.Width(115) );
var so : float = target.ODDQOODO[target.objectScript.OOQQQOQO[target.objectScript.ODOQOOQO]];
target.ODDQOODO[target.objectScript.OOQQQOQO[target.objectScript.ODOQOOQO]] = EditorGUILayout.Slider(target.ODDQOODO[target.objectScript.OOQQQOQO[target.objectScript.ODOQOOQO]], -50, 50, GUILayout.Width(175) );
EditorGUILayout.EndHorizontal();
if(target.objectScript.objectType == 2){
EditorGUILayout.BeginHorizontal();
GUILayout.Label(new GUIContent(" Sharp Corner", "This will create a sharp corner for the selected marker"), GUILayout.Width(95) );
ia = target.sharpCorner;
target.sharpCorner = EditorGUILayout.Toggle (target.sharpCorner);
EditorGUILayout.EndHorizontal();
if(ia != target.sharpCorner){
target.objectScript.OODQDODOCC();

RoadObjectEditorScript.ODQCOCOQQC(target.objectScript);


}
}
if(GUI.changed){
if(so != target.ODDQOODO[target.objectScript.OOQQQOQO[target.objectScript.ODOQOOQO]]){
ODCDDQQDDC.OCCQOCDQQO(target.objectScript.transform, target.objectScript.ODODDQOO[target.objectScript.ODOQOOQO]);
RoadObjectEditorScript.OOCQCCDOQQ(target.objectScript, target.objectScript.OOQQQOQO[target.objectScript.ODOQOOQO], target.objectScript);

}
}
}

}
function MarkerOnScene(markerScript : MarkerScript){
var cEvent : Event = Event.current;

if(!target.objectScript.ODODDDOO || target.objectScript.objectType == 2){
if(cEvent.shift && (target.objectScript.ODCDCODQOC == 0 || target.objectScript.ODCDCODQOC == 1)) {
Selection.activeGameObject =  markerScript.transform.parent.parent.gameObject;
}else if(cEvent.shift && target.objectScript.OCCCCCQQQD != target.transform){
target.objectScript.OOCDQOOQDQ(markerScript);
Selection.objects = target.objectScript.OCCCCCQQQDs;
}else if(target.objectScript.OCCCCCQQQD != target.transform && count == 0){
if(!target.InSelected()){
target.objectScript.OCCCCCQQQDs = new GameObject[0];
target.objectScript.OOCDQOOQDQ(markerScript);
Selection.objects = target.objectScript.OCCCCCQQQDs;


count++;
}

}else{

if(cEvent.control && !cEvent.alt)target.snapMarker = true;
else target.snapMarker = false;

pos = markerScript.oldPos;
if(pos  != oldPos && !markerScript.changed){
oldPos = pos;
if(!cEvent.shift){
target.objectScript.OQCDDDCOQO(markerScript);

if(target.objectScript.objectType == 2) RoadObjectEditorScript.ODQCOCOQQC(target.objectScript);	
}
}
}
if(cEvent.shift && markerScript.changed){
OQCDDDOODD = true;
}
markerScript.changed = false;
if(!cEvent.shift && OQCDDDOODD){
target.objectScript.OQCDDDCOQO(markerScript);

if(target.objectScript.objectType == 2) RoadObjectEditorScript.ODQCOCOQQC(target.objectScript); 	
OQCDDDOODD = false;
}
}

}
}
