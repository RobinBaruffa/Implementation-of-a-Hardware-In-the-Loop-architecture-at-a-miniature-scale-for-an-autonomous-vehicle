let SessionLoad = 1
if &cp | set nocp | endif
let s:so_save = &so | let s:siso_save = &siso | set so=0 siso=0
let v:this_session=expand("<sfile>:p")
silent only
silent tabonly
cd /root/Documents/Unity_ST40/Suratram_mixedRealitySimulator/Assets/Scripts
if expand('%') == '' && !&modified && line('$') <= 1 && getline(1) == ''
  let s:wipebuf = bufnr('%')
endif
set shortmess=aoO
argglobal
%argdel
set splitbelow splitright
set nosplitbelow
set nosplitright
wincmd t
set winminheight=0
set winheight=1
set winminwidth=0
set winwidth=1
tabnext 1
badd +109 Sensors/TruthSensor/TruthSensor.cs
badd +56 Export/JSONCreator.cs
badd +104 Sensors/SensorExport/SensorTruthExport.cs
badd +1 Sensors/SensorExport/SensorRadarExport.cs
badd +49 Sensors/Radar/RadarSensor.cs
badd +132 Behavours/SplineFollowing.cs
badd +38 Sensors/LidarSensor/SphericalCoordinate.cs
badd +60 /root/Documents/Unity_ST40/Suratram_sensorsProcessing/Assets/Scripts/Behavours/Platooning/TruthSpringPlatooning.cs
badd +1 /root/Documents/Unity_ST40/Suratram_sensorsProcessing/Assets/Scripts/SensorsProcessing/SensorProcessing/TruthSensorProcessingData.cs
badd +115 /root/Documents/Unity_ST40/Suratram_sensorsProcessing/Assets/Scripts/Behavours/Platooning/RadarOnlyPlatooning.cs
badd +40 /root/Documents/Unity_ST40/Suratram_sensorsProcessing/Assets/Scripts/SensorsProcessing/SensorProcessing/TruthSensorProcessing.cs
if exists('s:wipebuf') && len(win_findbuf(s:wipebuf)) == 0
  silent exe 'bwipe ' . s:wipebuf
endif
unlet! s:wipebuf
set winheight=1 winwidth=20 shortmess=filnxtToOS
set winminheight=1 winminwidth=1
let s:sx = expand("<sfile>:p:r")."x.vim"
if file_readable(s:sx)
  exe "source " . fnameescape(s:sx)
endif
let &so = s:so_save | let &siso = s:siso_save
nohlsearch
let g:this_session = v:this_session
let g:this_obsession = v:this_session
doautoall SessionLoadPost
unlet SessionLoad
" vim: set ft=vim :
