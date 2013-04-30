//
//  NativeXCore.m
//  Unity-iPhone
//
//  Created by Josh Ruis on 1/17/13.
//
//

#import "NativeXCore.h"
#import "NSObject-SBJSON.h"

UIViewController *UnityGetGLViewController();

void UnityPause( bool pause );

void UnitySendMessage( const char * className, const char * methodName, const char * param );

static NativeXCore *sharedInstance;

@implementation NativeXCore

@synthesize bannerView;

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
    [[NativeXMonetizationSDK sharedInstance] setShouldBeWebOfferWall:NO];
    [[NativeXMonetizationSDK sharedInstance] openOfferWallFromPresentingViewController:UnityGetGLViewController()];
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

-(void)showOfferWallFromPoint:(CGPoint)point
{
    CGRect myRect = CGRectMake(point.x, point.y, 25.0, 2.0);
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

-(void)getAndCacheInterstitial
{
    showInterstitial = NO;
    BOOL myOrientation = UIInterfaceOrientationIsLandscape(UnityGetGLViewController().interfaceOrientation);
    _myInterstitial = [[[NativeXMonetizationSDK sharedInstance] interstitialAdViewControllerWithThemeID:nil delegate:self]retain];
    _myInterstitial.landscapeOrientation = myOrientation;
    _myInterstitial.handleStatusBar = NO;    
    
}

-(void)showCachedInterstitial
{
    if(_myInterstitial !=NULL)
    {
        [_myInterstitial presentFromViewController:UnityGetGLViewController()];
    }
}

-(void)showInterstitial
{
    showInterstitial = YES;
    [self showInterstitialFromPoint:CGPointZero];
}

-(void)showInterstitialFromPoint:(CGPoint)point
{
    showInterstitial = YES;
    myPoint = point;
    BOOL myOrientation = UIInterfaceOrientationIsLandscape(UnityGetGLViewController().interfaceOrientation);
    
    _myInterstitial = [[[NativeXMonetizationSDK sharedInstance] interstitialAdViewControllerWithThemeID:nil delegate:self]retain];
    _myInterstitial.landscapeOrientation = myOrientation;
    _myInterstitial.handleStatusBar = NO;
    
}

-(void) showBannerWithRect:(CGRect)rect
{
    self.bannerView = [[NativeXMonetizationSDK sharedInstance] bannerAdViewWithThemeID:nil delegate:self frame:rect];
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
    //NSString *senderString = isAvailable ? @"1" : @"0";
    UnitySendMessage("NativeXHandler_iOS", "didSDKInitializeSuccessfully", "1");
}

-(void)nativeXMonetizationSdkDidFailToInitiate:(NSError *)error
{
    NSLog(@"SDK Inititalization failed with Error: %@", error);
    UnitySendMessage("NativeXHandler_iOS", "didSDKInitializeSuccessfully", "0");
}

-(void)didRedeemWithBalances:(NSArray *)balances andReceiptId:(NSString *)receiptId
{
    if(balances){
        NSString *myString = @"";
        myString = [myString stringByAppendingString:[balances nativeXPublisherJSONRepresentation]];
        NSLog(@"JSON(inXCode): %@", myString);
        UnitySendMessage("NativeXHandler_iOS", "didRedeemBalancesSuccessfully", [myString UTF8String]);
    }else{
        NSLog(@"No Balance Returned");
    }
    if(receiptId)
    {
        UnitySendMessage("NativeXHandler_iOS", "receiptId", [receiptId UTF8String]);
    }
    
}

-(void) didRedeemWithError:(NSError *)error
{
    NSLog(@"Redemption failed with Error: %@", error);
}

-(UIViewController *)parentViewControllerForModallyPresentingInterstitialInstructionView:(UIViewController *)interstitialInstructionVC
{
    NSLog(@"We have hit the Instuction View");
    return UnityGetGLViewController();
}

-(void)offerWallDidDisplay
{
    UnitySendMessage("NativeXHandler_iOS", "didOfferWallDisplay", "1");
}

-(void)offerWallWillDisplay
{
    UnitySendMessage("NativeXHandler_iOS", "didOfferWallDisplay", "0");
}

-(void)offerWallWillRedirectUserToAppStore
{
    UnitySendMessage("NativeXHandler_iOS", "willOfferWallRedirectUserToAppStore", "1");
}

-(void)offerWallWillDismiss
{
    UnitySendMessage("NativeXHandler_iOS", "didOfferDismiss", "0");
}

-(void)offerWallDidDismiss
{
    [self.pointView removeFromSuperview];
    self.pointView = nil;
    UnitySendMessage("NativeXHandler_iOS", "didOfferDismiss", "1");
}

-(void)featuredOfferIsAvailable
{
    UnitySendMessage("NativeXHandler_iOS", "isFeaturedOfferAvailable", "1");
}

-(void)featuredOfferNotAvailable
{
    UnitySendMessage("NativeXHandler_iOS", "isFeaturedOfferAvailable", "0");
}

-(void)featuredOfferDidDismiss
{
    NSLog(@"We have hit featuredOfferDidDismiss");
    UnitySendMessage("NativeXHandler_iOS", "didFeaturedOfferDismiss", "1");
}

//_________________________________________________________________________________________________
//Interstitial Delegates
//_________________________________________________________________________________________________

-(void)interstitialAdViewController:(NativeXInterstitialAdViewController *)adView didFailWithError:(NSError *)error
{
    NSLog(@"Interstitial failed to inititialize with Error: %@", error);
}

-(void)didLoadContentForInterstitialAdViewController:(NativeXInterstitialAdViewController *)adView
{
    UnitySendMessage("NativeXHandler_iOS", "doesAdContentForInterstitialAdViewExist", "1");
    if(showInterstitial == YES)
    {
        [_myInterstitial presentFromViewController:UnityGetGLViewController()];
    }
}

-(void)noAdContentForInterstitialAdViewController:(NativeXInterstitialAdViewController *)adView
{
    NSLog(@"We were unable to load any content for Interstitial.");
    UnitySendMessage("NativeXHandler_iOS", "doesAdContentForInterstitialAdViewExist", "0");
}

-(void)didDismissForInterstitialAdViewController:(NativeXInterstitialAdViewController *)adView
{
    _myInterstitial = nil;
    UnitySendMessage("NativeXHandler_iOS", "didInterstitialDismiss", "1");
}

-(void)dismissActionForInterstitialAdViewController:(NativeXInterstitialAdViewController *)adView
{
    UnitySendMessage("NativeXHandler_iOS", "didInterstitialDismiss", "1");
}

-(BOOL)interstitialAdViewController:(NativeXInterstitialAdViewController *)adView shouldLeaveApplicationOpeningURL:(NSURL *)url
{
    return YES;
}

//_________________________________________________________________________________________________
//Banner Delegates
//_________________________________________________________________________________________________

-(void)didLoadContentForBannerAdView:(NativeXBannerAdView *)adView
{
    if(bannerView) {
        [bannerView setHidden:NO];
    }
    UnitySendMessage("NativeXHandler_iOS", "doesAdContentForBannerAdViewExist", "1");
}

-(void)noAdContentForBannerAdView:(NativeXBannerAdView *)adView
{
    if(bannerView) {
        [bannerView setHidden:YES];
    }
    UnitySendMessage("NativeXHandler_iOS", "doesAdContentForBannerAdViewExist", "0");
}

-(void)bannerAdView:(NativeXBannerAdView *)adView didFailWithError:(NSError *)error
{
    NSLog(@"Banner failed with Error: %@", error);
}

-(BOOL)bannerAdView:(NativeXBannerAdView *)adView shouldLeaveApplicationOpeningURL:(NSURL *)url
{
    return YES;
}

//_________________________________________________________________________________________________
//Tracking Delegates
//_________________________________________________________________________________________________

-(void)trackInAppPurchaseDidSucceedForRequest:(NativeXInAppPurchaseTrackRequest *)inAppPurchaseRequest
{
    NSLog(@"Tracking did Succeed");
    UnitySendMessage("NativeXHandler_iOS", "didTrackInAppPurchaseSucceed", "1");
}

-(void)trackInAppPurchaseForRequest:(NativeXInAppPurchaseTrackRequest *)inAppPurchaseRequest didFailWithError:(NSError *)error
{
    NSLog(@"Tracking failed with Error: %@", error);
    UnitySendMessage("NativeXHandler_iOS", "didTrackInAppPurchaseSucceed", "0");
}

@end
