#!/usr/bin/python
from mod_pbxproj import XcodeProject

project = XcodeProject.Load(ARGV[1])

writer = ""

writer += ("Argv1 = "+ARGV[1])
#writer += "testUnity"
print ARGV[1]
#print "test"

f = open('/Users/josh.ruis/Documents/UnityTest.txt' , 'w')
f.write(writer)
f.close()
