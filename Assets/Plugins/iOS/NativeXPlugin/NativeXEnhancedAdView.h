//
//  NativeXEnhancedAdView.h
//  NativeXMonetizationSdk
//
//  Created by Ash Lindquist on 6/4/13.
//
//

#import <UIKit/UIKit.h>

/** Ad placement type.
 */
typedef enum
{
    /// Ad is placed in application content.
    nativeXAdViewPlacementTypeInline = 0,
    
    /// Ad is placed over and in the way of application content.
    /// Generally used to place an ad between transtions in an application
    /// and consumes the entire screen.
    nativeXAdViewPlacementTypeInterstitial
    
} nativeXAdViewPlacementType;

@protocol NativeXEnhancedAdViewDelegate;

//TODO: we should consolidate the NativeXAdView into this class
@interface NativeXEnhancedAdView : UIView

@property (nonatomic, weak) id<NativeXEnhancedAdViewDelegate> delegate;

@property (nonatomic, readonly) UIButton *closeButton;
@property (nonatomic, readonly) nativeXAdViewPlacementType placementType;

// TODO: add comments about what "name" means
@property (nonatomic, strong) NSString *name;
@property (nonatomic, strong) UIViewController *presentingViewController;

- (id)initInterstitialAdWithName:(NSString *)name
                        delegate:(id<NativeXEnhancedAdViewDelegate>)aDelegate;

//called to display the Interstitial once the content has been loaded
- (void)displayInterstitial;

- (void)displayInterstitial:(UIViewController *) presentingVC;

//called do
- (void)dismissInterstitial;

- (BOOL) isVisible;

@end

@protocol NativeXEnhancedAdViewDelegate <NSObject>

@optional
//use this method to override when adView is displayed
- (void)didLoadEnhancedAdView:(NativeXEnhancedAdView *)adView withName:(NSString *)name;

//notification that no ad was available at this time
- (void)noAdContentForEnhancedAdView:(NativeXEnhancedAdView *)adView;

//error loading an ad (was the SDK initialized correctly?)
- (void)enhancedAdView:(NativeXEnhancedAdView *)adView didFailWithError:(NSError *)error;

//notification the ad content has expired
- (void)adContentExpiredForEnhancedAdView:(NativeXEnhancedAdView *)adView;


//adview defaults to using the keyWindow's rootviewcontroller, but this method can be used to set a specific parent view controller for the AdView
- (UIViewController *)presentingViewControllerForAdView:(NativeXEnhancedAdView *) adView;

//adview lifecycle events
- (void)enhancedAdViewWillDisplay:(NativeXEnhancedAdView *)adView;
- (void)enhancedAdViewDidDisplay:(NativeXEnhancedAdView *)adView;
- (void)enhancedAdViewWillDismiss:(NativeXEnhancedAdView *)adView;
- (void)enhancedAdViewDidDismiss:(NativeXEnhancedAdView *)adView;


//for overriding MRAID events
//TODO: do we need this?
- (void)enhancedAdView:(NativeXEnhancedAdView *)adView didProcessRichmediaRequest:(NSURLRequest*)event;

@end

