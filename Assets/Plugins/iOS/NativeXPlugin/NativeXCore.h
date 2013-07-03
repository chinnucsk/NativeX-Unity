//
//  NativeXCore.h
//  Unity-iPhone
//
//  Created by Josh Ruis on 1/17/13.
//
//

#import <Foundation/Foundation.h>
#import "NativeXMonetizationSDK.h"
#import "NativeXInterstitialAdViewController.h"
#import "NativeXBannerAdView.h"
#import "NativeXEnhancedAdView.h"
//
//extern UIView * UnityGetGLView();
//extern UIViewController * UnityGetGLViewController();

@interface NativeXCore : NSObject <NativeXMonetizationDelegate, NativeXInterstitialAdViewControllerDelegate, NativeXBannerAdViewDelegate, NativeXInAppPurchaseTrackDelegate, NativeXEnhancedAdViewDelegate >
{
@private
    BOOL showInterstitial;
    NativeXBannerAdView *bannerView;
    CGPoint myPoint;
}

+ (NativeXCore*) instance;

-(void)startWithName:(NSString*)name applicationId:(NSString*)appId publisherId:(NSString*)pubId;
-(void)showOfferWall;
-(void)showIncentOfferWall;
-(void)showNonIncentOfferWall;
-(void)showOfferWallFromPoint;
-(void)getAndCacheFeaturedOffer;
-(void)showCachedFeaturedOffer;
-(void)showFeaturedOffer;
-(void)getAndCacheInterstitial;
-(void)getAndCacheEnhancedInterstitial:(NSString*)name;
-(void)showCachedInterstitial;
-(void)showCachedEnhancedInterstitial:(NSString*)name;
-(void)showInterstitial;
-(void)showEnhancedInsterstitial:(NSString*)name;
-(void)showBanner;
-(void)removeBanner;
-(void)actionTaken:(NSString*)actionId;
-(void)connectWithAppId:(NSString*)appId;
-(void)redeemCurrency;
-(void)trackInAppPurchase:(NativeXInAppPurchaseTrackRecord*)record;
-(void)close;

@property (nonatomic, retain) NativeXBannerAdView *bannerView;
@property (nonatomic, retain) NativeXInterstitialAdViewController *myInterstitial;
@property (nonatomic, retain) NativeXEnhancedAdView *myAdView;
@property (nonatomic, retain) UIView *pointView;
@property (nonatomic) CGPoint bannerPoint;
@property (nonatomic) CGPoint offerWallPoint;
@property (nonatomic) float bannerHeight;
@property (nonatomic) float bannerWidth;
@property (nonatomic, retain) NSDictionary *interstitialDic;


@end
