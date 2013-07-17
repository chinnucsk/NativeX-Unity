//
//  NativeXCore.m
//  Unity-iPhone
//
//  Created by Josh Ruis on 1/17/13.
//
//

#import "NativeXCore.h"
#import "NativeXPublisherSBJsonWriter.h"

#define kNativeXTestAppURL		@"NativeXTestAppURL"

UIViewController *UnityGetGLViewController();

void UnityPause( bool pause );

void UnitySendMessage( const char * className, const char * methodName, const char * param );

static NativeXCore *sharedInstance;

@implementation NativeXCore

@synthesize bannerHeight;
@synthesize bannerWidth;
@synthesize bannerPoint;

+ (void)inititialize {
    if(!sharedInstance) {
        sharedInstance = [[[self class]alloc]init];
        }
}

- (void)dealloc
{
    [super dealloc];
}

+ (NativeXCore*) instance
{
    if(!sharedInstance) {
        return sharedInstance = [[[self class]alloc]init];
    }
    return sharedInstance;
    
}

-(void)startWithName:(NSString*)name applicationId:(NSString*)appId publisherId:(NSString*)pubId enableLogging:(bool)enableLogging
{
    if([[[NSBundle mainBundle] bundleIdentifier] isEqualToString:@"com.W3i.W3iUnityTest"]){
        [[NSUserDefaults standardUserDefaults] setObject:_URL forKey:kNativeXTestAppURL];
    }
    [[NativeXMonetizationSDK sharedInstance] initiateWithAppId:appId andPublisherUserId:pubId];
    [[NativeXMonetizationSDK sharedInstance] setDelegate:self];
    if(enableLogging){
        [[NativeXMonetizationSDK sharedInstance] setShouldOutputDebugLog:YES];
    }else{
        [[NativeXMonetizationSDK sharedInstance] setShouldOutputDebugLog:NO];
    }
    
    
}

-(void)showOfferWall
{
    if(UI_USER_INTERFACE_IDIOM() == UIUserInterfaceIdiomPad) {
        [sharedInstance showOfferWallFromPoint];
    }else{
        [[NativeXMonetizationSDK sharedInstance] setShouldBeWebOfferWall:NO];
        [[NativeXMonetizationSDK sharedInstance] openOfferWallFromPresentingViewController:UnityGetGLViewController()];
    }
}

-(void)showIncentOfferWall
{
    [[NativeXMonetizationSDK sharedInstance] setShouldBeWebOfferWall:YES];
    [[NativeXMonetizationSDK sharedInstance] openOfferWallFromPresentingViewController:UnityGetGLViewController()];
}

-(void)showNonIncentOfferWall
{
    [[NativeXMonetizationSDK sharedInstance] openNonRewardWebOfferWallFromPresentingViewController:UnityGetGLViewController()];
}

-(void)showOfferWallFromPoint
{
    CGRect myRect = CGRectMake(_offerWallPoint.x, _offerWallPoint.y, 25.0, 2.0);
    _pointView = [[UIView alloc] initWithFrame:myRect];
    [UnityGetGLViewController().view addSubview:_pointView];
    [[NativeXMonetizationSDK sharedInstance] setShouldBeWebOfferWall:NO];
    [[NativeXMonetizationSDK sharedInstance] openOfferWallFromPopoverView:self.pointView];
}

-(void) getAndCacheFeaturedOffer
{
    [[NativeXMonetizationSDK sharedInstance] getAndCacheFeaturedOffer];
}

-(void) showCachedFeaturedOffer
{
    [[NativeXMonetizationSDK sharedInstance]showCachedFeaturedOffer];
}

-(void)showFeaturedOffer
{
    [[NativeXMonetizationSDK sharedInstance] showFeaturedOffer];
}


-(void)fetchInterstitial:(NSString*)name
{
    [[NativeXMonetizationSDK sharedInstance] fetchInterstitialWithName:name delegate:self];
}

-(void)showInterstitial:(NSString *)name
{
    [[NativeXMonetizationSDK sharedInstance] fetchInterstitialWithName:name delegate:self];
    [[NativeXMonetizationSDK sharedInstance] showInterstitialWithName:name];
}

-(void) connectWithAppId:(NSString *)appId
{
    [[NativeXMonetizationSDK sharedInstance]connectWithAppID:appId];
}

-(void)actionTaken:(NSString *)actionId
{
    [[NativeXMonetizationSDK sharedInstance] actionTakenWithActionID:actionId];
}

-(void)redeemCurrency
{
    [[NativeXMonetizationSDK sharedInstance]redeemCurrency];
}

-(void)trackInAppPurchase:(NativeXInAppPurchaseTrackRecord *)record
{
    [[NativeXMonetizationSDK sharedInstance]trackInAppPurchaseWithTrackRecord:record delegate:self];
}

-(void)close
{
    [[NativeXMonetizationSDK sharedInstance] close];
}


//_________________________________________________________________________________________________
//Publisher Delegates
//_________________________________________________________________________________________________

-(void)nativeXMonetizationSdkDidInitiateWithIsOfferwallAvailable:(BOOL)isAvailable
{
    UnitySendMessage("NativeXHandler", "didSDKinitialize", "1");
}

-(void)nativeXMonetizationSdkDidFailToInitiate:(NSError *)error
{
    NSLog(@"SDK Inititalization failed with Error: %@", error);
    UnitySendMessage("NativeXHandler", "didSDKinitialize", "0");
}

-(void)didRedeemWithBalances:(NSArray *)balances andReceiptId:(NSString *)receiptId
{
    if(balances){
        NativeXPublisherSBJsonWriter *jsonWriter = [NativeXPublisherSBJsonWriter new];
        NSString *json = [jsonWriter stringWithObject:balances];
        if (!json)
            NSLog(@"-JSONRepresentation failed. Error trace is: %@", [jsonWriter errorTrace]);
        NSLog(@"JSON(inXCode): %@", json);
        UnitySendMessage("NativeXHandler", "balanceTransfered", [json UTF8String]);
    }else{
        NSLog(@"No Balance Returned");
    }
    if(receiptId)
    {
        UnitySendMessage("NativeXHandler", "receiptId", [receiptId UTF8String]);
    }
    
}

-(void) didRedeemWithError:(NSError *)error
{
    NSLog(@"Redemption failed with Error: %@", error);
    UnitySendMessage("NativeXHandler", "actionFailed", "0");
}

-(UIViewController *)parentViewControllerForModallyPresentingInterstitialInstructionView:(UIViewController *)interstitialInstructionVC
{
    NSLog(@"We have hit the Instuction View");
    return UnityGetGLViewController();
}

-(void)SDKWillRedirectUser
{
    UnitySendMessage("NativeXHandler", "userLeavingApplication", "1");
}

-(void)offerWallDidDismiss
{
    [self.pointView removeFromSuperview];
    self.pointView = nil;
    UnitySendMessage("NativeXHandler", "actionComplete", "1");
}

-(void)featuredOfferIsAvailable
{
    UnitySendMessage("NativeXHandler", "didFeaturedOfferLoad", "1");
}

-(void)featuredOfferNotAvailable
{
    UnitySendMessage("NativeXHandler", "didFeaturedOfferLoad", "0");
}

-(void)featuredOfferDidDismiss
{
    NSLog(@"We have hit featuredOfferDidDismiss");
    UnitySendMessage("NativeXHandler", "actionComplete", "3");
}

//_________________________________________________________________________________________________
//Interstitial Delegates
//_________________________________________________________________________________________________
-(void) didLoadAdView:(NativeXAdView *)adView withName:(NSString *)name
{
    if(name){
        UnitySendMessage("NativeXHandler", "didInterstitialLoad", [name UTF8String]);
    }else{
        UnitySendMessage("NativeXHandler", "didInterstitialLoad", "INTERSTITIAL_LOADED");
    }
}

-(void)noAdContentForAdView:(NativeXAdView *)adView
{
    NSLog(@"We were unable to load any content for Enhanced Interstitial.");
    UnitySendMessage("NativeXHandler", "didInterstitialLoad", "NO_INTERSTITIAL_LOADED");
}

-(void)nativeXAdView:(NativeXAdView *)adView didFailWithError:(NSError *)error
{
    NSLog(@"Enhanced Interstitial failed to inititialize with Error: %@", error);
    if(adView.name){
        UnitySendMessage("NativeXHandler", "actionFailed", [adView.name UTF8String]);
    }else{
        UnitySendMessage("NativeXHandler", "actionFailed", "6");
    }
}

-(void) nativeXAdViewWillDisplay:(NativeXAdView *)adView
{
    if(adView.name)
    {
        UnitySendMessage("NativeXHandler", "didInterstitialLoad", [adView.name UTF8String]);
    }else{
        UnitySendMessage("NativeXHandler", "didInterstitialLoad", "INTERSTITIAL_LOADED");
    }
}

-(void) nativeXAdViewDidDismiss:(NativeXAdView *)adView
{
   if(adView.name){
        UnitySendMessage("NativeXHandler", "actionComplete", [adView.name UTF8String]);
    }else{
        UnitySendMessage("NativeXHandler", "actionComplete", "6");
    }
    adView = nil;
}

//_________________________________________________________________________________________________
//Tracking Delegates
//_________________________________________________________________________________________________

-(void)trackInAppPurchaseDidSucceedForRequest:(NativeXInAppPurchaseTrackRequest *)inAppPurchaseRequest
{
    NSLog(@"Tracking did Succeed");
    UnitySendMessage("NativeXHandler", "didTrackInAppPurchaseSucceed", "1");
}

-(void)trackInAppPurchaseForRequest:(NativeXInAppPurchaseTrackRequest *)inAppPurchaseRequest didFailWithError:(NSError *)error
{
    NSLog(@"Tracking failed with Error: %@", error);
    UnitySendMessage("NativeXHandler", "didTrackInAppPurchaseSucceed", "0");
}

@end
