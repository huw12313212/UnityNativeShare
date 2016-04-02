#import "UnityAppController.h"
#import <AVFoundation/AVFoundation.h>
#import <AssetsLibrary/AssetsLibrary.h>
#import <MediaPlayer/MediaPlayer.h>


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


void MergeFile(const char* videoPath,const char* audioPath)
{
    NSLog(@"Try to merge files");
    
    NSString *videoPathStr = [NSString stringWithUTF8String:videoPath];
    NSURL *videourl = [NSURL fileURLWithPath:videoPathStr];
    
    NSString *audioPathStr = [NSString stringWithUTF8String:audioPath];
    NSURL *audiourl = [NSURL fileURLWithPath:audioPathStr];
    
    //Create AVMutableComposition Object which will hold our multiple AVMutableCompositionTrack or we can say it will hold our video and audio files.
    AVMutableComposition* mixComposition = [AVMutableComposition composition];
    
    //Now first load your audio file using AVURLAsset. Make sure you give the correct path of your videos.
    AVURLAsset  *audioAsset = [[AVURLAsset alloc]initWithURL:audiourl options:nil];
    CMTimeRange audio_timeRange = CMTimeRangeMake(kCMTimeZero, audioAsset.duration);
    
    //Now we are creating the first AVMutableCompositionTrack containing our audio and add it to our AVMutableComposition object.
    AVMutableCompositionTrack *b_compositionAudioTrack = [mixComposition addMutableTrackWithMediaType:AVMediaTypeAudio preferredTrackID:kCMPersistentTrackID_Invalid];
    [b_compositionAudioTrack insertTimeRange:audio_timeRange ofTrack:[[audioAsset tracksWithMediaType:AVMediaTypeAudio] objectAtIndex:0] atTime:kCMTimeZero error:nil];
    
    //Now we will load video file.
    AVURLAsset  *videoAsset = [[AVURLAsset alloc]initWithURL:videourl options:nil];
    CMTimeRange video_timeRange = CMTimeRangeMake(kCMTimeZero,audioAsset.duration);
    
    //Now we are creating the second AVMutableCompositionTrack containing our video and add it to our AVMutableComposition object.
    AVMutableCompositionTrack *a_compositionVideoTrack = [mixComposition addMutableTrackWithMediaType:AVMediaTypeVideo preferredTrackID:kCMPersistentTrackID_Invalid];
    [a_compositionVideoTrack insertTimeRange:video_timeRange ofTrack:[[videoAsset tracksWithMediaType:AVMediaTypeVideo] objectAtIndex:0] atTime:kCMTimeZero error:nil];
    
    //decide the path where you want to store the final video created with audio and video merge.
    NSArray *dirPaths = NSSearchPathForDirectoriesInDomains(NSDocumentDirectory, NSUserDomainMask, YES);
    NSString *docsDir = [dirPaths objectAtIndex:0];
    NSString *outputFilePath = [docsDir stringByAppendingPathComponent:[NSString stringWithFormat:@"FinalVideo.mov"]];
    NSURL *outputFileUrl = [NSURL fileURLWithPath:outputFilePath];
    if ([[NSFileManager defaultManager] fileExistsAtPath:outputFilePath])
        [[NSFileManager defaultManager] removeItemAtPath:outputFilePath error:nil];
    
    //Now create an AVAssetExportSession object that will save your final video at specified path.
    AVAssetExportSession* _assetExport = [[AVAssetExportSession alloc] initWithAsset:mixComposition presetName:AVAssetExportPresetHighestQuality];
    _assetExport.outputFileType = @"com.apple.quicktime-movie";
    _assetExport.outputURL = outputFileUrl;
    
    [_assetExport exportAsynchronouslyWithCompletionHandler:
     ^(void ) {
         
         dispatch_async(dispatch_get_main_queue(), ^{
             
             NSLog(@"files done");
             
             UnityAppController* unityController = ((UnityAppController*)([UIApplication sharedApplication].delegate));
             
             
             UIActivityViewController * activities = [[UIActivityViewController alloc]
                                                      initWithActivityItems:@[outputFileUrl]
                                                      applicationActivities:nil];
             
             activities.popoverPresentationController.sourceView = (UIView*) unityController.unityView;
             
             
             [unityController.rootViewController presentViewController:activities
                                                              animated:YES
                                                            completion:nil];
             
         });
     }
     ];
    
}


