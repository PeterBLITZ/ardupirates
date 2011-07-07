$dir = "C:/Users/hog/Documents/Arduino/libraries/GCS_MAVLink/include/common/";


opendir(DIR,$dir) || die print $!;
@files = readdir(DIR);
closedir(DIR);

push(@files,"../mavlink_types.h");

open(OUT,">MAVLinkTypes.cs");

print OUT <<EOF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace ArdupilotMega
{
    partial class MAVLink
    {
EOF

foreach $file (@files) {
	print "$file\n";
	
	open(F,$dir.$file);
	
	$start = 0;
	
	while ($line = <F>) {
		if ($line =~ /enum (MAV_.*)/) {
			$start = 1;
			print OUT "\t\tpublic ";
		}
	
		if ($line =~ /#define (MAVLINK_MSG_ID[^\s]+)\s+([0-9]+)/) {
			print OUT "\t\tpublic const byte ".$1 . " = " . $2 . ";\n";
			$no = $2;
		}
		if ($line =~ /typedef struct(.*)/) {
			if ($1 =~ /__mavlink_system/) {
				last;
			}
			$start = 1;
			print OUT "\t\t[StructLayout(LayoutKind.Sequential,Pack=1)]\n";
			#__mavlink_gps_raw_t
			$structs[$no] = $1;
		}
		if ($start) {
			$line =~ s/MAV_CMD_NAV_//;
			
			$line =~ s/MAV_CMD_//;
		
			$line =~ s/typedef/public/;
			$line =~ s/uint8_t/public byte/;
			$line =~ s/int8_t/public byte/;
			$line =~ s/^\s+float/public float/;
			$line =~ s/uint16_t/public ushort/;
			$line =~ s/uint32_t/public uint/;
			$line =~ s/uint64_t/public ulong/;
			$line =~ s/int16_t/public short/;
			$line =~ s/int32_t/public int/;
			$line =~ s/int64_t/public long/;
			$line =~ s/typedef/public/;
			
			$line =~ s/}.*/};\n/;
			
			if ($line =~ /\[(.*)\].*;/) { # array
				  print OUT "\t\t[MarshalAs(
				    	UnmanagedType.ByValArray,
				    	SizeConst=". $1 .")] \n";
				    	$line =~ s/\[.*\]//;
				    	$line =~ s/public\s+([^\s]+)/public $1\[\]/o;
			}
			
			print OUT "\t\t".$line;
		}		
		if ($line =~ /}/) {
			$start = 0;
		}
	
	}
	
	close(F);
}

print OUT "Type[] mavstructs = new Type[] {";
for ($a = 0; $a <= 256;$a++)
{
	if (defined($structs[$a])) {
		print OUT "typeof(".$structs[$a] .") ,";
	} else {
		print OUT "null ,";
	}
}
print OUT "};\n\n";

print OUT <<EOF;
	}
}

EOF

close OUT;

1;