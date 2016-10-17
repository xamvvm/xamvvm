clean:
	rm -f *.nupkg
	rm -rf */bin
	rm -rf */obj
	rm -rf */*/bin
	rm -rf */*/obj
	rm -rf */*/*/bin
	rm -rf */*/*/obj
	rm -rf */*/*/*/bin
	rm -rf */*/*/*/obj	
	
packages:

	nuget pack source/Xamvvm.Core.nuspec
	nuget pack source/Xamvvm.Forms.nuspec
	nuget pack source/Xamvvm.Mock.nuspec
	nuget pack source/Xamvvm.Forms.RxUI.nuspec
