//
//  NativeXCore.m
//  Unity-iPhone
//
//  Created by Josh Ruis on 1/17/13.
//
//

#import "NativeXCore.h"
//#import "NSObject-SBJSON.h"
#import "NativeXPublisherSBJsonWriter.h"

UIViewController *UnityGetGLViewController();

void UnityPause( bool pause );

void UnitySendMessage( const char * className, const char * methodName, const char * param );

static NativeXCore *sharedInstance;

@implementation NativeXCore

@synthesize bannerView;
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
    [_myInterstitial release];
    _myInterstitial = nil;
    [super dealloc];
}

+ (NativeXCore*) instance
{
    if(!sharedInstance) {
        return sharedInstance = [[[self class]alloc]init];
    }
    return sharedInstance;
    
}

-(void)startWithName:(NSString*)name applicationId:(NSString*)appId publisherId:(NSString*)pubId
{
    [[NativeXMonetizationSDK sharedInstance] initiateWithAppId:appId andPublisherUserId:pubId];
    [[NativeXMonetizationSDK sharedInstance] setDelegate:self];
    
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


-(void)getAndCacheInterstitial:(NSString*)name
{
    showInterstitial = NO;
    [[NativeXMonetizationSDK sharedInstance] cacheInterstitialAdWithName:name delegate:self];
}


-(void)showCachedInterstitial:(NSString*)name
{
    [[NativeXMonetizationSDK sharedInstance] showInterstitialAdWithName:name];
}


-(void)showInterstitial:(NSString *)name
{
    showInterstitial = YES;
    [[NativeXMonetizationSDK sharedInstance] cacheInterstitialAdWithName:name delegate:self];
}

-(void)showBanner
{
    self.bannerView = [[NativeXMonetizationSDK sharedInstance] bannerAdViewWithThemeID:nil delegate:self frame:CGRectMake(bannerPoint.x, bannerPoint.y, bannerWidth, bannerHeight)];
    bannerView.hidden = YES;
    [UnityGetGLViewController().view addSubview:bannerView];
    
    
}

-(void)removeBanner
{
    [bannerView removeFromSuperview];
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

-(void)offerWallDidDisplay
{
    UnitySendMessage("NativeXHandler", "actionComplete", "1");
}

-(void)offerWallWillDisplay
{
    UnitySendMessage("NativeXHandler", "actionComplete", "1");
}

-(void)offerWallWillRedirectUserToAppStore
{
    UnitySendMessage("NativeXHandler", "userLeavingApplication", "1");
}

-(void)offerWallWillDismiss
{
    UnitySendMessage("NativeXHandler", "actionComplete", "1");
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
//Enhanced Interstitial Delegates
//_________________________________________________________________________________________________
-(void) didLoadEnhancedAdView:(NativeXEnhancedAdView *)adView withName:(NSString *)name
{
    if(showInterstitial == YES)
    {
        [[NativeXMonetizationSDK sharedInstance] showInterstitialAdWithName:name];
    }
    if(name){
        UnitySendMessage("NativeXHandler", "didInterstitialLoad", [name UTF8String]);
    }else{
        UnitySendMessage("NativeXHandler", "didInterstitialLoad", "INTERSTITIAL_LOADED");
    }
}

-(void)noAdContentForEnhancedAdView:(NativeXEnhancedAdView *)adView
{
    NSLog(@"We were unable to load any content for Enhanced Interstitial.");
    UnitySendMessage("NativeXHandler", "didInterstitialLoad", "NO_INTERSTITIAL_LOADED");
}

-(void)enhancedAdView:(NativeXEnhancedAdView *)adView didFailWithError:(NSError *)error
{
    NSLog(@"Enhanced Interstitial failed to inititialize with Error: %@", error);
    if(adView.name){
        UnitySendMessage("NativeXHandler", "actionFailed", [adView.name UTF8String]);
    }else{
        UnitySendMessage("NativeXHandler", "actionFailed", "6");
    }
}

-(void) enhancedAdViewWillDisplay:(NativeXEnhancedAdView *)adView
{
    if(adView.name)
    {
        UnitySendMessage("NativeXHandler", "didInterstitialLoad", [adView.name UTF8String]);
    }else{
        UnitySendMessage("NativeXHandler", "didInterstitialLoad", "INTERSTITIAL_LOADED");
    }
}

-(void) enhancedAdViewDidDismiss:(NativeXEnhancedAdView *)adView
{
   if(adView.name){
        UnitySendMessage("NativeXHandler", "actionComplete", [adView.name UTF8String]);
    }else{
        UnitySendMessage("NativeXHandler", "actionComplete", "6");
    }
    adView = nil;
}

//_________________________________________________________________________________________________
//Banner Delegates
//_________________________________________________________________________________________________

-(void)didLoadContentForBannerAdView:(NativeXBannerAdView *)adView
{
    bannerView = adView;
    if(bannerView) {
        [bannerView setHidden:NO];
    }
    UnitySendMessage("NativeXHandler", "didBannerLoad", "1");
}

-(void)noAdContentForBannerAdView:(NativeXBannerAdView *)adView
{
    if(bannerView) {
        [bannerView setHidden:YES];
    }
    UnitySendMessage("NativeXHandler", "didBannerLoad", "0");
}

-(void)bannerAdView:(NativeXBannerAdView *)adView didFailWithError:(NSError *)error
{
    NSLog(@"Banner failed with Error: %@", error);
    UnitySendMessage("NativeXHandler", "actionFailed", "7");
}

-(BOOL)bannerAdView:(NativeXBannerAdView *)adView shouldLeaveApplicationOpeningURL:(NSURL *)url
{
    UnitySendMessage("NativeXHandler", "userLeavingApplication", "7");
    return YES;
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
