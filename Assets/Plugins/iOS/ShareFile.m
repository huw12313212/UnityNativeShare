#import "UnityAppController.h"

bool ShareFile( const char * path )
{
	NSString *imagePath = [NSString stringWithUTF8String:path];
    NSURL *url = [NSURL fileURLWithPath:imagePath];

    NSLog(@"###### This is the file path being passed: %@", imagePath);

    UnityAppController* unityController = ((UnityAppController*)([UIApplication sharedApplication].delegate));
    
    
    UIActivityViewController * activities = [[UIActivityViewController alloc]
                                             initWithActivityItems:@[url]
                                             applicationActivities:nil];
    
    activities.popoverPresentationController.sourceView = (UIView*) unityController.unityView;
    
    
    [unityController.rootViewController presentViewController:activities
                                  animated:YES
                                completion:nil];
    
    return false;
}


