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
//
//extern UIView * UnityGetGLView();
//extern UIViewController * UnityGetGLViewController();

@interface NativeXCore : NSObject <NativeXMonetizationDelegate, NativeXInterstitialAdViewControllerDelegate, NativeXBannerAdViewDelegate, NativeXInAppPurchaseTrackDelegate>
{
@private
    BOOL showInterstitial;
    bool featuredOfferIsAvailable;
    NativeXBannerAdView *bannerView;
    CGPoint myPoint;
}

+ (NativeXCore*) instance;

-(void)startWithName:(NSString*)name applicationId:(NSString*)appId publisherId:(NSString*)pubId;
-(void)showOfferWall;
-(void)showIncentOfferWall;
-(void)showNonIncentOfferWall;
-(void)showOfferWallFromPoint:(CGPoint)point;
-(void)getAndCacheFeaturedOffer;
-(void)showCachedFeaturedOffer;
-(void)showFeaturedOffer;
-(void)getAndCacheInterstitial;
-(void)showCachedInterstitial;
-(void)showInterstitial;
-(void)showInterstitialFromPoint:(CGPoint)point;
-(void)showBannerWithRect:(CGRect)rect;
-(void)removeBanner;
-(void)actionTaken:(NSString*)actionId;
-(void)connectWithAppId:(NSString*)appId;
-(void)redeemCurrency;
-(void)trackInAppPurchase:(NativeXInAppPurchaseTrackRecord*)record;
-(void)close;

@property (nonatomic, retain) NativeXBannerAdView *bannerView;
@property (nonatomic, retain) NativeXInterstitialAdViewController *myInterstitial;
@property (nonatomic, retain) UIView *pointView;

@end
